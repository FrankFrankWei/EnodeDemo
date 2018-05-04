using ENode.Commanding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationBC.Commands.Departments
{
    public class RemoveDepartmentChildCommand : Command<string>
    {
        public string ChildDepartmentId { get; set; }

        private RemoveDepartmentChildCommand() { }

        public RemoveDepartmentChildCommand(string departmentId,string childDepartmentId):base(departmentId)
        {
            ChildDepartmentId = childDepartmentId;
        }
    }
}
