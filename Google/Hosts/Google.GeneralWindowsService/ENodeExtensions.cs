using ECommon.Components;
using ECommon.Logging;
using ECommon.Scheduling;
using ECommon.Socketing;
using ENode.Commanding;
using ENode.Configurations;
using ENode.EQueue;
using ENode.Infrastructure;
using EQueue.Broker;
using EQueue.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ENode.Eventing;
using System.Configuration;
using EQueue.Clients.Producers;
using EQueue.Clients.Consumers;
using Google.Infrastructure;
using EQueue.NameServer;
using EQueue.Utils;
using ECommon.Extensions;
using Google.Infrastructure.Configs;
using OrganizationBC.Commands;
using OrganizationBC.Domains;

namespace Google.GeneralWindowsService
{
    public static class ENodeExtensions
    {
        private static BrokerController _broker;
        private static CommandService _commandService;
        private static CommandConsumer _commandConsumer;
        private static DomainEventPublisher _eventPublisher;
        private static DomainEventConsumer _eventConsumer;
        private static NameServerController _nameServer;

        public static object EQueueTopics { get; private set; }

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

            var brokerStorePath = ConfigurationManager.AppSettings["equeueStorePath"];

            var brokerSetting = new BrokerSetting(chunkFileStoreRootPath: brokerStorePath);
            brokerSetting.BrokerInfo.ProducerAddress = new IPEndPoint(IPAddress.Loopback, int.Parse(ConfigurationManager.AppSettings["producerPort"])).ToAddress();
            brokerSetting.BrokerInfo.ConsumerAddress = new IPEndPoint(IPAddress.Loopback, int.Parse(ConfigurationManager.AppSettings["consumerPort"])).ToAddress();
            brokerSetting.BrokerInfo.AdminAddress = new IPEndPoint(IPAddress.Loopback, int.Parse(ConfigurationManager.AppSettings["adminPort"])).ToAddress();
            brokerSetting.NameServerList = nameServerEndpoints;

            var nameServiceSetting = new NameServerSetting()
            {

                BindingAddress = nameServerEndpoints.First()
            };

            _nameServer = new NameServerController(nameServiceSetting);

            _broker = BrokerController.Create(brokerSetting);


            _commandService = new CommandService(setting: producerSetting);

            _eventPublisher = new DomainEventPublisher(setting: producerSetting);

            configuration.SetDefault<ICommandService, CommandService>(_commandService);
            configuration.SetDefault<IMessagePublisher<DomainEventStreamMessage>, DomainEventPublisher>(_eventPublisher);

            _commandConsumer = new CommandConsumer(setting: consumerSetting);
            _eventConsumer = new DomainEventConsumer(setting: consumerSetting) { };

            //TODO 设置消息Topic
            _commandConsumer
                .Subscribe(CommandTopicContainer.EmployeeCommandTopic.TopicName)
                .Subscribe(CommandTopicContainer.DepartmentCommandTopic.TopicName);
            _eventConsumer
                .Subscribe(EventTopicContainer.EmployeeEventTopic.TopicName)
                .Subscribe(EventTopicContainer.DepartmentEventTopic.TopicName);

            return enodeConfiguration;
        }
        public static ENodeConfiguration StartEQueue(this ENodeConfiguration enodeConfiguration)
        {
            _nameServer.Start();
            _broker.Start();
            _eventConsumer.Start();
            _commandConsumer.Start();
            _eventPublisher.Start();
            _commandService.Start();

            //WaitAllConsumerLoadBalanceComplete();

            return enodeConfiguration;
        }
        public static ENodeConfiguration ShutdownEQueue(this ENodeConfiguration enodeConfiguration)
        {
            _commandService.Shutdown();
            _eventPublisher.Shutdown();
            _commandConsumer.Shutdown();
            _eventConsumer.Shutdown();
            _broker.Shutdown();
            _nameServer.Shutdown();
            return enodeConfiguration;
        }

        private static void WaitAllConsumerLoadBalanceComplete()
        {
            var logger = ObjectContainer.Resolve<ILoggerFactory>().Create(typeof(ENodeExtensions).Name);
            var scheduleService = ObjectContainer.Resolve<IScheduleService>();
            var waitHandle = new ManualResetEvent(false);
            logger.Info("Waiting for all consumer load balance complete, please wait for a moment...");
            scheduleService.StartTask("WaitAllConsumerLoadBalanceComplete", () =>
            {
                var eventConsumerAllocatedQueues = _eventConsumer.Consumer.GetCurrentQueues();
                var commandConsumerAllocatedQueues = _commandConsumer.Consumer.GetCurrentQueues();
                if (eventConsumerAllocatedQueues.Count() == 4 && commandConsumerAllocatedQueues.Count() == 4)
                {
                    waitHandle.Set();
                }
            }, 1000, 1000);

            waitHandle.WaitOne();
            scheduleService.StopTask("WaitAllConsumerLoadBalanceComplete");
            logger.Info("All consumer load balance completed.");
        }
    }
}
