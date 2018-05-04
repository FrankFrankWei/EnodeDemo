using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrganizationBC.Domains.Departments;
using ENode.Domain;
using ECommon.Components;
using OrganizationBC.DTOs;
using Dapper;

namespace OrganizationBC.Domains.Dapper
{
    [Component]
    public class DepartmentSnapRepository : BaseSnapRepository, IAggregateRepository<Department>
    {
        public Department Get(string aggregateRootId)
        {
            using (var conn = GetConnection())
            {
                var sql = "select * from Department where Id = @Id";
                var departmentDTO = conn.Query<DepartmentDTO>(sql, new { Id = aggregateRootId }).FirstOrDefault();
                var employeeIds = conn.Query<string>("select Id from Employee where DepartmentId=@DepartmentId", new { DepartmentId = aggregateRootId }).ToList();
                var childIds = conn.Query<string>("select Id from Department where ParentId=@ParentId", new { ParentId = aggregateRootId }).ToList();
                return Department.CreateInstance(departmentDTO.Id, departmentDTO.Name, departmentDTO.SortIndex, departmentDTO.ParentId, employeeIds, childIds, departmentDTO.Version);
            }
        }
    }
}
