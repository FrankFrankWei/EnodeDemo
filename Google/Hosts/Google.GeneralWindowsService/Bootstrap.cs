using ECommon.Components;
using ECommon.Configurations;
using ECommon.Logging;
using ENode.Configurations;
using ENode.Infrastructure;
using EQueue.Broker;
using EQueue.Configurations;
using Google.Infrastructure.Configs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ECommonConfiguration = ECommon.Configurations.Configuration;

namespace Google.GeneralWindowsService
{
    public class Bootstrap
    {
        static ILogger _logger;
        static ENodeConfiguration _configuration;

        public static void Initialize()
        {

            try
            {
                InitializeENodeFramework();
                InitializeCommandService();
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
                _configuration.StartEQueue();
            }
            catch (Exception ex)
            {
                _logger.Error(" EQueue start failed.", ex);
                throw;
            }
        }
        public static void Stop()
        {
            try
            {
                if (_configuration != null)
                {
                    _configuration.ShutdownEQueue();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Broker stop failed.", ex);
                throw;
            }
        }

        private static void InitializeENodeFramework()
        {
            ConfigSettings.Initialize();
            var assemblies = new[]
            {
                Assembly.Load("OrganizationBC.DTOs"),
                Assembly.Load("OrganizationBC.Domains"),
                Assembly.Load("OrganizationBC.Commands"),
                Assembly.Load("OrganizationBC.CommandHandlers"),
                Assembly.Load("OrganizationBC.EventHandlers"),
                Assembly.Load("OrganizationBC.ProcessManagers"),
                Assembly.Load("OrganizationBC.Domains.Dapper"),
                Assembly.GetExecutingAssembly()
            };
            var setting = new ConfigurationSetting(ConfigSettings.ENodeConnectionString);
            _configuration = ECommon.Configurations.Configuration
                .Create()
                .UseAutofac()
                .RegisterCommonComponents()
                .UseLog4Net()
                .UseJsonNet()
                .RegisterUnhandledExceptionHandler()
                .CreateENode(setting)
                .RegisterENodeComponents()
                .RegisterBusinessComponents(assemblies)
                //.UseSnapshotOnlyAggregateStorage()
                .UseSqlServerLockService()
                .UseSqlServerEventStore()
                .UseSqlServerPublishedVersionStore()
                .UseEQueue()
                .InitializeBusinessAssemblies(assemblies);
            _logger = ObjectContainer.Resolve<ILoggerFactory>().Create(typeof(Bootstrap).FullName);
            _logger.Info("ENode initialized.");
        }

        private static void InitializeCommandService()
        {
            // ObjectContainer.Resolve<ILockService>().AddLockKey(typeof(Admin).Name);
            _logger.Info("GeneralWindowsService initialized.");
        }
    }
}
