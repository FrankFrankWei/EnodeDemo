using ENode.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationBC.Domains.Employees
{
    public class EmployeeRemoved : DomainEvent<string>
    {
        public string DepartmentId { get; private set; }

        private EmployeeRemoved() { }
        public EmployeeRemoved(string departmentId)
        {
            DepartmentId = departmentId;
        }
    }
}
