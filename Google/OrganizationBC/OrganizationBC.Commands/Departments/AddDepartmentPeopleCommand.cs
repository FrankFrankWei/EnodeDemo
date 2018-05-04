using ENode.Commanding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationBC.Commands.Departments
{
    public class AddDepartmentPeopleCommand :Command<string>
    {
        public string EmployeeId { get; set; }

        private AddDepartmentPeopleCommand() { }

        public AddDepartmentPeopleCommand(string departmentId,string employeeId) : base(departmentId)
        {
            EmployeeId = employeeId;
        }
    }
}
