using ENode.Commanding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationBC.Commands.Departments
{
    public class RemoveDepartmentCommand : Command<string>
    {

        private RemoveDepartmentCommand() { }

        public RemoveDepartmentCommand(string departmentId) : base(departmentId)
        {
        }
    }
}
