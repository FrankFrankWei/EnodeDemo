using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrganizationBC.DTOs;
using Google.Infrastructure.Paged;

namespace OrganizationBC.QueryServices
{
    public interface IEmployeeQueryService
    {
        bool? IsExist(string employeeId);

        EmployeeDTO Find(string employeeId);

        EmployeeDTO FindByUserName(string userName);

        PagedData<EmployeeDTO> GetEmployeeList(int pageIndex, int pageSize);
    }
}
