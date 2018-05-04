using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENode.Domain;
using OrganizationBC.Domains.Employees;
using Dapper;
using OrganizationBC.DTOs;
using ECommon.Components;

namespace OrganizationBC.Domains.Dapper
{
    [Component]
    public class EmployeeSnapRepository : BaseSnapRepository,IAggregateRepository<Employee>
    {
        public Employee Get(string aggregateRootId)
        {
            using(var conn = GetConnection())
            {
                var sql = "select * from Employee where Id = @Id";
               var employeeDTO = conn.Query<EmployeeDTO>(sql, new { Id = aggregateRootId }).FirstOrDefault();
                return Employee.CreateInstance(employeeDTO.Id, employeeDTO.UserName, employeeDTO.Password, employeeDTO.UserName, employeeDTO.Sex, employeeDTO.Status, employeeDTO.DepartmentId, employeeDTO.Verion);
            }
        }
    }
}
