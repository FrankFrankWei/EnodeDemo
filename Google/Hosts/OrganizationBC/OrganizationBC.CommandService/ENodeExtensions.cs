using ENode.Configurations;
using ENode.EQueue;
using ENode.Eventing;
using ENode.Infrastructure;
using EQueue.Clients.Consumers;
using EQueue.Clients.Producers;
using EQueue.Configurations;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System;
using OrganizationBC.Commands;

namespace OrganizationBC.CommandService
{
    public static class ENodeExtensions
    {
        private static CommandConsumer _commandConsumer;
        private static DomainEventPublisher _eventPublisher;

        public static ENodeConfiguration UseEQueue(this ENodeConfiguration enodeConfiguration)
        {
            var configuration = enodeConfiguration.GetCommonConfiguration();

            configuration.RegisterEQueueComponents();

            var nameServerAddresses = ConfigurationManager.AppSettings["nameServerAddress"];
            var nameServerEndpoints = new List<IPEndPoint>();
            var servers = nameServerAddresses.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in servers)
            {
                var addressInfo = item.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                var nameServerEndpoint = new IPEndPoint(IPAddress.Parse(addressInfo[0]), int.Parse(addressInfo[1]));
                nameServerEndpoints.Add(nameServerEndpoint);
            }
            ProducerSetting producerSetting = new ProducerSetting()
            {
                NameServerList = nameServerEndpoints
            };

            _eventPublisher = new DomainEventPublisher(setting: producerSetting);
            configuration.SetDefault<IMessagePublisher<DomainEventStreamMessage>, DomainEventPublisher>(_eventPublisher);

            ConsumerSetting consumerSetting = new ConsumerSetting()
            {
                NameServerList = nameServerEndpoints
            };
            var consumerGroupName = ConfigurationManager.AppSettings["GroupName"];
            _commandConsumer = new CommandConsumer(groupName: string.IsNullOrEmpty(consumerGroupName) ? null : consumerGroupName, setting: consumerSetting);
            //TODO 设置命令消息订阅的Topic
            _commandConsumer
                .Subscribe(CommandTopicContainer.EmployeeCommandTopic.TopicName)
                .Subscribe(CommandTopicContainer.DepartmentCommandTopic.TopicName);

            return enodeConfiguration;
        }
        public static ENodeConfiguration StartEQueue(this ENodeConfiguration enodeConfiguration)
        {
            _commandConsumer.Start();
            _eventPublisher.Start();
            return enodeConfiguration;
        }
        public static ENodeConfiguration ShutdownEQueue(this ENodeConfiguration enodeConfiguration)
        {
            _commandConsumer.Shutdown();
            _eventPublisher.Shutdown();
            return enodeConfiguration;
        }
    }
}
