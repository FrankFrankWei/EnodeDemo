using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Google.Infrastructure.Configs
{
    public class ConfigSettings
    {

        public static string ReadDBConnectionString { get; set; }
        public static string ENodeConnectionString { get; set; }

        public static void Initialize()
        {
            ReadDBConnectionString = ConfigurationManager.ConnectionStrings["readdb"].ConnectionString;
            ENodeConnectionString = ConfigurationManager.ConnectionStrings["enodedb"].ConnectionString;
        }
    }
}
