using Google.Infrastructure.Configs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationBC.Domains.Dapper
{
    public class BaseSnapRepository
    {
        protected IDbConnection GetConnection()
        {
            return new SqlConnection(ConfigSettings.ReadDBConnectionString);
        }
    }
}
