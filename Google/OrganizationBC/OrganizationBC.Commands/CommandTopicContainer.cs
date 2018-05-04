using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Infrastructure.Message;
using OrganizationBC.Commands.Employees;
using OrganizationBC.Commands.Departments;

namespace OrganizationBC.Commands
{
    public class CommandTopicContainer
    {
        //TODO 在这里把各种命令划分好Topic,使用XXX.Infrastructure.Message.TopicInfo类划分

        public static MessageTopicInfo EmployeeCommandTopic = new MessageTopicInfo(
            "EmployeeCommandTopic", 
            typeof(CreateEmployeeCommand), 
            typeof(ChangeEmployeePasswordCommand),
            typeof(ChangeEmployeeBaseInfoCommand),
            typeof(DisableEmployeeCommand),
            typeof(EnableEmplyeeCommand),
            typeof(RemoveEmployeeCommand)
            );

        public static MessageTopicInfo DepartmentCommandTopic = new MessageTopicInfo(
            "DepartmentCommandTopic",
            typeof(AddDepartmentChildCommand),
            typeof(AddDepartmentPeopleCommand),
            typeof(CreateDepartmentCommand),
            typeof(RemoveDepartmentChildCommand),
            typeof(RemoveDepartmentCommand),
            typeof(RemoveDepartmentPeopleCommand)
            );
    }
}
