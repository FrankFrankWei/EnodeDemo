using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENode.Commanding;
using Google.Infrastructure.Enums;

namespace OrganizationBC.Commands.Employees
{
    public class CreateEmployeeCommand : Command<string>
    {
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public Sex Sex { get; set; }

        public string RealName { get; private set; }

        public string DepartmentId { get; private set; }

        private CreateEmployeeCommand() { }

        public CreateEmployeeCommand(string employeeId,string userName, string password, Sex sex, string realName, string departmentId):base(employeeId)
        {
            UserName = userName;
            Password = password;
            Sex = sex;
            RealName = realName;
            DepartmentId = departmentId;
        }
    }
}
