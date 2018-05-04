using ECommon.Components;
using ECommon.Configurations;
using ECommon.Extensions;
using ECommon.Logging;
using EQueue.Broker;
using EQueue.Configurations;
using EQueue.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ECommonConfiguration = ECommon.Configurations.Configuration;

namespace Google.BrokerService
{
    public class Bootstrap
    {
        private static ILogger _logger;
        private static ECommonConfiguration _ecommonConfiguration;
        private static BrokerController _broker;

        public static void Initialize()
        {
            InitializeECommon();
            try
            {
                InitializeEQueue();
            }
            catch (Exception ex)
            {
                _logger.Error("Initialize EQueue failed.", ex);
                throw;
            }
        }
        public static void Start()
        {
            try
            {
                _broker.Start();
            }
            catch (Exception ex)
            {
                _logger.Error("Broker start failed.", ex);
                throw;
            }
        }
        public static void Stop()
        {
            try
            {
                if (_broker != null)
                {
                    _broker.Shutdown();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Broker stop failed.", ex);
                throw;
            }
        }

        private static void InitializeECommon()
        {
            _ecommonConfiguration = ECommonConfiguration
                .Create()
                .UseAutofac()
                .RegisterCommonComponents()
                .UseLog4Net()
                .UseJsonNet()
                .RegisterUnhandledExceptionHandler();
            _logger = ObjectContainer.Resolve<ILoggerFactory>().Create(typeof(Bootstrap).FullName);
            _logger.Info("ECommon initialized.");
        }
        private static void InitializeEQueue()
        {
            _ecommonConfiguration.RegisterEQueueComponents();
            var nameServerAddresses = ConfigurationManager.AppSettings["nameServerAddress"];
            var nameServerEndpoints = new List<IPEndPoint>();
            var servers = nameServerAddresses.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in servers)
            {
                var addressInfo = item.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                var nameServerEndpoint = new IPEndPoint(IPAddress.Parse(addressInfo[0]), int.Parse(addressInfo[1]));
                nameServerEndpoints.Add(nameServerEndpoint);
            }
            var brokerStorePath = ConfigurationManager.AppSettings["equeueStorePath"];

            var brokerSetting = new BrokerSetting(chunkFileStoreRootPath: brokerStorePath);
            brokerSetting.BrokerInfo.ProducerAddress = new IPEndPoint(IPAddress.Loopback, int.Parse(ConfigurationManager.AppSettings["producerPort"])).ToAddress();
            brokerSetting.BrokerInfo.ConsumerAddress = new IPEndPoint(IPAddress.Loopback, int.Parse(ConfigurationManager.AppSettings["consumerPort"])).ToAddress();
            brokerSetting.BrokerInfo.AdminAddress = new IPEndPoint(IPAddress.Loopback, int.Parse(ConfigurationManager.AppSettings["adminPort"])).ToAddress();

            brokerSetting.NameServerList = nameServerEndpoints;
            brokerSetting.BrokerInfo.GroupName = ConfigurationManager.AppSettings["groupName"];
            brokerSetting.BrokerInfo.BrokerName = ConfigurationManager.AppSettings["brokerName"];
            _broker = BrokerController.Create(brokerSetting);
            _logger.Info("EQueue initialized.");
        }
    }
}
