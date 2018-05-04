using ENode.Commanding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationBC.Commands.Departments
{ 
    public class AddDepartmentChildCommand : Command<string>
    {
        public string ChildDepartmentId { get; set; }

        private AddDepartmentChildCommand() { }

        public AddDepartmentChildCommand(string departmentId, string childDepartmentId):base(departmentId)
        {
            ChildDepartmentId = childDepartmentId;
        }
    }
}
