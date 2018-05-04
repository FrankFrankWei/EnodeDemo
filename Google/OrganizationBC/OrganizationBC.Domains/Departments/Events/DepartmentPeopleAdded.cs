using ENode.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationBC.Domains.Departments
{
    public class DepartmentPeopleAdded : DomainEvent<string>
    {
        public string EmployeeId { get; private set; }
        public int Amount { get; private set; }

        private DepartmentPeopleAdded() { }

        public DepartmentPeopleAdded(string employeeId,int amount)
        {
            EmployeeId = employeeId;
            Amount = amount;
        }
    }
}
