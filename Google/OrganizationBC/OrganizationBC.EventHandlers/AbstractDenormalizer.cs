using ECommon.IO;
using Google.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.Common;
using Google.Infrastructure.Configs;

namespace OrganizationBC.EventHandlers
{
    public abstract class AbstractDenormalizer
    {
        protected async Task<AsyncTaskResult> TryInsertRecordAsync(Func<IDbConnection, Task<long>> action)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    await action(connection);
                    return AsyncTaskResult.Success;
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)  //主键冲突，忽略即可；出现这种情况，是因为同一个消息的重复处理
                {
                    return AsyncTaskResult.Success;
                }
                throw;
            }
        }
        protected async Task<AsyncTaskResult> TryUpdateRecordAsync(Func<IDbConnection, Task<int>> action)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    await action(connection);
                    return AsyncTaskResult.Success;
                }
            }
            catch
            {
                throw;
            }
        }

        protected async Task<AsyncTaskResult> TryTransactionAsync(Func<IDbConnection, IDbTransaction, IEnumerable<Task>> actions)
        {
            using (var connection = GetSqlConnection())
            {
                await connection.OpenAsync().ConfigureAwait(false);
                var transaction = await Task.Run<SqlTransaction>(() => connection.BeginTransaction()).ConfigureAwait(false);
                try
                {
                    await Task.WhenAll(actions(connection, transaction)).ConfigureAwait(false);
                    await Task.Run(() => transaction.Commit()).ConfigureAwait(false);
                    return AsyncTaskResult.Success;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        protected IDbConnection GetConnection()
        {
            return new SqlConnection(ConfigSettings.ReadDBConnectionString);
        }

        private SqlConnection GetSqlConnection()
        {
            return new SqlConnection(ConfigSettings.ReadDBConnectionString);
        }
    }
}
