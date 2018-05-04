using OrganizationBC.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationBC.QueryServices
{
    public interface IDepartmentQueryService
    {
        bool? IsExist(string departmentId);
        DepartmentDTO Find(string departmentId);

        List<DepartmentDTO> GetAllDepartmentList();
    }
}
