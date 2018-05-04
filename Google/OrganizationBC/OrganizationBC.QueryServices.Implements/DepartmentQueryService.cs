using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrganizationBC.DTOs;
using ECommon.Dapper;
using Dapper;

namespace OrganizationBC.QueryServices.Implements
{
    [ECommon.Components.Component]
    public class DepartmentQueryService : AbstractQueryService, IDepartmentQueryService
    {
        public DepartmentDTO Find(string departmentId)
        {
            using (var conn = GetConnection())
            {
                return conn.QueryList<DepartmentDTO>(new { Id = departmentId }, "Department", "*").FirstOrDefault();
            }
        }

        public List<DepartmentDTO> GetAllDepartmentList()
        {
            throw new NotImplementedException();
        }

        public bool? IsExist(string departmentId)
        {
            using (var conn = GetConnection())
            {
                return conn.Query<string>("select Id from Department where Id = @DepartmentId", new { DepartmentId = departmentId }) != null;
            }
        }
    }
}
