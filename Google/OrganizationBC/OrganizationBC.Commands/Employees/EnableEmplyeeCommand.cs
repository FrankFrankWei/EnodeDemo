using ENode.Commanding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationBC.Commands.Employees
{
    public class EnableEmplyeeCommand : Command<string>
    {
        private EnableEmplyeeCommand() { }

        public EnableEmplyeeCommand(string employeeId) : base(employeeId)
        {

        }
    }
}
