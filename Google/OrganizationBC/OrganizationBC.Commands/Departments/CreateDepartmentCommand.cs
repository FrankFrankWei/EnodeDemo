using ENode.Commanding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationBC.Commands.Departments
{
    public class CreateDepartmentCommand : Command<string>
    {
        public string Name { get; set; }
        public string ParentId { get; set; }
        public int SortIndex { get; set; }

        private CreateDepartmentCommand() { }
        public CreateDepartmentCommand(string departmentId,string name,string parentId,int sortIndex):base(departmentId)
        {
            Name = name;
            ParentId = parentId;
            SortIndex = sortIndex;
        }
    }
}
