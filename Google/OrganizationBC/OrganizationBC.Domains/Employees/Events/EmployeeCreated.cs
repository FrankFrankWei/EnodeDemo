using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENode.Eventing;
using Google.Infrastructure.Enums;

namespace OrganizationBC.Domains.Employees
{
    public class EmployeeCreated : DomainEvent<string>
    {
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public Sex Sex { get; set; }

        public string RealName { get; private set; }
        public EmployeeStatus Status { get; private set; }

        public string DepartmentId { get; private set; }

        private EmployeeCreated() { }

        public EmployeeCreated(string userName,string password,Sex sex,string realName,EmployeeStatus status,string departmentId)
        {
            UserName = userName;
            Password = password;
            Sex = sex;
            RealName = realName;
            Status = status;
            DepartmentId = departmentId;
        }



    }
}
