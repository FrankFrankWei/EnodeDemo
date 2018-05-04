using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Infrastructure.Paged;
using OrganizationBC.DTOs;
using ECommon.Dapper;
using Dapper;
namespace OrganizationBC.QueryServices.Implements
{
    [ECommon.Components.Component]
    public class EmployeeQueryService : AbstractQueryService, IEmployeeQueryService
    {
        public EmployeeDTO Find(string employeeId)
        {
            using(var conn = GetConnection())
            {
                return conn.QueryList<EmployeeDTO>(new { Id = employeeId }, "Employee", "*").FirstOrDefault();
            }
        }

        public EmployeeDTO FindByUserName(string userName)
        {
            using (var conn = GetConnection())
            {
                return conn.QueryList<EmployeeDTO>(new { UserName = userName }, "Employee", "*").FirstOrDefault();
            }
        }

        public PagedData<EmployeeDTO> GetEmployeeList(int pageIndex, int pageSize)
        {
            using (var conn = GetConnection())
            {
                var count = conn.GetCount(null, "Employee");
                if (count > 0)
                {
                    pageIndex = RevisePageIndex(pageIndex, pageSize, count);
                    return conn.QueryPaged<EmployeeDTO>(null, "Employee", "CreatedOn desc", pageIndex, pageSize).ToList().ToPagedData(pageIndex, pageSize, count);
                }
                return null;
            }
        }

        public bool? IsExist(string employeeId)
        {
            using (var conn = GetConnection())
            {
                return conn.Query<string>("select Id from Employee where Id = @EmployeeId",new { EmployeeId = employeeId  })!=null;
            }
        }
    }
}
