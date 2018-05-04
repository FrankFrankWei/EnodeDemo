using ENode.Commanding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationBC.Commands.Departments
{
    public class RemoveDepartmentPeopleCommand :Command<string>
    {
        public string EmployeeId { get; set; }

        private RemoveDepartmentPeopleCommand() { }

        public RemoveDepartmentPeopleCommand(string departmentId, string employeeId) : base(departmentId)
        {
            EmployeeId = employeeId;
        }
    }
}
