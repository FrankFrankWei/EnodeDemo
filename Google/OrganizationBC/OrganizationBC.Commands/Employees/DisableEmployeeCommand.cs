using ENode.Commanding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationBC.Commands.Employees
{
    public class DisableEmployeeCommand : Command<string>
    {
        private DisableEmployeeCommand() { }

        public DisableEmployeeCommand(string employeeId) : base(employeeId)
        {

        }
    }
}
