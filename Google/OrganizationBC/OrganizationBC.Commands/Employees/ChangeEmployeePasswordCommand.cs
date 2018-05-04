using ENode.Commanding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationBC.Commands.Employees
{
    public class ChangeEmployeePasswordCommand :Command<string>
    {
        public string Password { get; private set; }

        private ChangeEmployeePasswordCommand() { }

        public ChangeEmployeePasswordCommand(string employeeId,string password) 
        {
            AggregateRootId = employeeId;
            Password = password;
        }
    }
}
