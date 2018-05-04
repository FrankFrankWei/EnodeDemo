using ENode.Eventing;
using Google.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationBC.Domains.Employees
{
    public class EmployeeStatusChanged :DomainEvent<string>
    {
        public EmployeeStatus Status { get; set; }

        private EmployeeStatusChanged() { }
        public EmployeeStatusChanged(EmployeeStatus status)
        {
            Status = status;
        }
    }
}
