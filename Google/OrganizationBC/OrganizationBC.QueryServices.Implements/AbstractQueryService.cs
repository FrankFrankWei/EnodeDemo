using Dapper;
using ECommon.Components;
using Google.Infrastructure;
using Google.Infrastructure.Configs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationBC.QueryServices.Implements
{
    [Component]
    public abstract class AbstractQueryService
    {
        protected IDbConnection GetConnection()
        {
            return new SqlConnection(ConfigSettings.ReadDBConnectionString);
        }
        /// <summary>
        /// 修正页码
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalItem"></param>
        /// <returns></returns>
        protected int RevisePageIndex(int pageIndex, int pageSize, int totalItem)
        {
            if (pageIndex < 0)
            {
                return 1;
            }
            var totalPage = (int)Math.Ceiling(totalItem / (double)pageSize);
            if (pageIndex > totalPage)
            {
                return totalPage;
            }
            return pageIndex;
        }
    }

    public static class DapperExtentsion
    {
        /// <summary>
        /// 查单表分页列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="conditionString">条件字符串(不能带where)</param>
        /// <param name="conditionValue"></param>
        /// <param name="table">表名</param>
        /// <param name="orderBy">排序字符串</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="columns">查询的列</param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static IEnumerable<T> QueryPaged<T>(this IDbConnection connection, string conditionString, object conditionValue, string table, string orderBy, int pageIndex, int pageSize, string columns = "*", IDbTransaction transaction = null, int? commandTimeout = null)
        {
            //conditionString = conditionString.ToUpper().Replace("WHERE", "");
            var sql = string.Format("SELECT {0} FROM (SELECT ROW_NUMBER() OVER (ORDER BY {1}) AS RowNumber, {0} FROM {2} WHERE {3}) AS Total WHERE RowNumber >= {4} AND RowNumber <= {5}", columns, orderBy, table, conditionString, (pageIndex - 1) * pageSize + 1, pageIndex * pageSize);
            return connection.Query<T>(sql, conditionValue, transaction, true, commandTimeout);
        }
    }
}
