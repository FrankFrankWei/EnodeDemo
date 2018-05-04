using ENode.Commanding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationBC.Commands.Employees
{
    public class RemoveEmployeeCommand : Command<string>
    {
        private RemoveEmployeeCommand() { }

        public RemoveEmployeeCommand(string employeeId) : base(employeeId)
        {

        }
    }
}
