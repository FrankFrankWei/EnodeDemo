using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENode.Domain;
using Google.Infrastructure.Enums;
using Google.Infrastructure;
using Google.Infrastructure.Encrypts;

namespace OrganizationBC.Domains.Employees
{
    public class Employee :AggregateRoot<string>
    {
        private string _userName;
        private string _password;
        private string _realName;
        private Sex _sex;
        private EmployeeStatus _status;
        private string _departmentId;
        private bool _isRemoved;
        

        private Employee(string employeeId,string userName,string password,string realName,Sex sex,EmployeeStatus status,string departmentId,int version) : base(employeeId, version)
        {
            _userName = userName;
            _password = password;
            _realName = realName;
            _sex = sex;
            _status = status;
            _departmentId = departmentId;
        }

        public static Employee CreateInstance(string employeeId, string userName, string password, string realName, Sex sex, EmployeeStatus status, string departmentId, int version)
        {
            return new Employee(employeeId, userName, PasswordHash.CreateHash(password), realName, sex, status, departmentId, version);
        }

        public Employee(string employeeId,string userName,string password,string realName,Sex sex,string departmentId) : base(employeeId)
        {
            Assert.IsNotNullOrEmpty("realName", realName);
            Assert.IsNotNullOrEmpty("password", userName);
            Assert.IsNotNullOrEmpty("userName", password);
            if (password.Length < 6)
            {
                throw new Exception("密码不能小于6位");
            }
            ApplyEvent(new EmployeeCreated(userName, PasswordHash.CreateHash(password), sex, realName, EmployeeStatus.Valid, departmentId));
        }
        
        public void ChangePassword(string password)
        {
            if (password.Length < 6)
            {
                throw new Exception("密码不能小于6位");
            }
            ApplyEvent(new EmployeePasswordChanged(PasswordHash.CreateHash(password)));
        }

        public void ChangeStatus(EmployeeStatus status)
        {
            if (!_status.Equals(status))
            {
                ApplyEvent(new EmployeeStatusChanged(status));
            }
        }

        public void ChangeBaseInfo(string realName,Sex sex,string departmentId)
        {
            Assert.IsNotNullOrEmpty("realName", realName);
            ApplyEvent(new EmployeeBaseInfoChanged(_departmentId, departmentId, realName, sex));
        }

        public void Remove()
        {
            if (!_isRemoved)
            {
                ApplyEvent(new EmployeeRemoved(_departmentId));
            }
        }

        private void Handle(EmployeeCreated evnt)
        {
            _userName = evnt.UserName;
            _password = evnt.Password;
            _realName = evnt.RealName;
            _sex = evnt.Sex;
            _status = evnt.Status;
            _departmentId = evnt.DepartmentId;
            _isRemoved = false;
        }


        private void Handle(EmployeeBaseInfoChanged evnt)
        {
            _realName = evnt.RealName;
            _sex = evnt.Sex;
            _departmentId = evnt.NewDepartmentId;
        }

        private void Handle(EmployeeStatusChanged evnt)
        {
            _status = evnt.Status;
        }

        private void Handle(EmployeeRemoved evnt)
        {
            _isRemoved = true;
        }

        public void Handle(EmployeePasswordChanged evnt)
        {
            _password = evnt.Password;
        }
    }
}
