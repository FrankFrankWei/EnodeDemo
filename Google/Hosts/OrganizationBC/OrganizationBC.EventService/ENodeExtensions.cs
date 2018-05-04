using ENode.Commanding;
using ENode.Configurations;
using ENode.EQueue;
using EQueue.Clients.Consumers;
using EQueue.Clients.Producers;
using EQueue.Configurations;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System;

namespace OrganizationBC.EventService
{
    public static class ENodeExtensions
    {
        private static CommandService _commandService;
        private static DomainEventConsumer _eventConsumer;

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
                NameServerList = nameServerEndpoints,

            };

            ConsumerSetting consumerSetting = new ConsumerSetting()
            {
                NameServerList = nameServerEndpoints
            };

            _commandService = new CommandService(setting: producerSetting);
            configuration.SetDefault<ICommandService, CommandService>(_commandService);

            var consumerGroupName = ConfigurationManager.AppSettings["GroupName"];
            _eventConsumer = new DomainEventConsumer(groupName: string.IsNullOrEmpty(consumerGroupName) ? null : consumerGroupName, setting: consumerSetting);
            //TODO 设置事件消息订阅的Topic
            _eventConsumer
                .Subscribe(OrganizationBC.Domains.EventTopicContainer.EmployeeEventTopic.TopicName)
                .Subscribe(OrganizationBC.Domains.EventTopicContainer.DepartmentEventTopic.TopicName);

            return enodeConfiguration;
        }
        public static ENodeConfiguration StartEQueue(this ENodeConfiguration enodeConfiguration)
        {
            _commandService.Start();
            _eventConsumer.Start();
            return enodeConfiguration;
        }
        public static ENodeConfiguration ShutdownEQueue(this ENodeConfiguration enodeConfiguration)
        {
            _eventConsumer.Shutdown();
            _commandService.Shutdown();
            return enodeConfiguration;
        }
    }
}
