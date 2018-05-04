using ENode.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationBC.Domains.Departments
{
    public class DepartmentChildRemoved : DomainEvent<string>
    {
        public string DepartmentId { get; private set; }
        public int Amount { get; private set; }

        private DepartmentChildRemoved() { }

        public DepartmentChildRemoved(string departmentId, int amount)
        {
            DepartmentId = departmentId;
            Amount = amount;
        }
    }
}
