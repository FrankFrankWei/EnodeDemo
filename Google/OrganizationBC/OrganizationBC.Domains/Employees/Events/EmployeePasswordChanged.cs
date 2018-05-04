using ENode.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationBC.Domains.Employees
{
    public class EmployeePasswordChanged :DomainEvent<string>
    {
        public string Password { get; private set; }

        private EmployeePasswordChanged() { }
        public EmployeePasswordChanged(string password)
        {
            Password = password;
        }
    }
}
