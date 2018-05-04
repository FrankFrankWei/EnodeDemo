using ECommon.Socketing;
using ENode.Commanding;
using ENode.Configurations;
using ENode.EQueue;
using EQueue.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using EQueue.Clients.Producers;

namespace Google.Web.Extensions
{
    public static class ENodeExtensions
    {
        private static CommandService _commandService;

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

            var processorPort = int.Parse(ConfigurationManager.AppSettings["CommandResultProcessorPort"]);
            _commandService = new CommandService(new CommandResultProcessor(new IPEndPoint(IPAddress.Loopback, processorPort)), producerSetting);

            configuration.SetDefault<ICommandService, CommandService>(_commandService);

            return enodeConfiguration;
        }
        public static ENodeConfiguration StartEQueue(this ENodeConfiguration enodeConfiguration)
        {
            _commandService.Start();
            return enodeConfiguration;
        }
    }
}
