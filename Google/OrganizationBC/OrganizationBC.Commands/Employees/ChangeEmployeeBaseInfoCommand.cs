using ENode.Commanding;
using Google.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationBC.Commands.Employees
{
    public class ChangeEmployeeBaseInfoCommand : Command<string>
    {
        public string RealName { get; set; }
        public Sex Sex { get; set; }

        public string DepartmentId { get; set; }

        private ChangeEmployeeBaseInfoCommand() { }

        public ChangeEmployeeBaseInfoCommand(string employeeId,string departmentId,string realName,Sex sex) : base(employeeId)
        {
            DepartmentId = departmentId;
            RealName = realName;
            Sex = sex;
        }
    }
}
