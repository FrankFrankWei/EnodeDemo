using ENode.Eventing;
using Google.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationBC.Domains.Employees
{
    public class EmployeeBaseInfoChanged : DomainEvent<string>
    {
        public string OldDepartmentId { get; private set; }
        public string NewDepartmentId { get; private set; }
        public string RealName { get; private set; }

        public Sex  Sex { get; private set; }

        private EmployeeBaseInfoChanged() { }
        public EmployeeBaseInfoChanged(string oldDepartmentId,string newDepartmentId,string realName,Sex sex)
        {
            OldDepartmentId = oldDepartmentId;
            NewDepartmentId = newDepartmentId;
            RealName = realName;
            Sex = sex;
        }

    }
}
