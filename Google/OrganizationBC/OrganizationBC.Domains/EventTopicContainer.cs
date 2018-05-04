using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Infrastructure.Message;
using OrganizationBC.Domains.Employees;
using OrganizationBC.Domains.Departments;
namespace OrganizationBC.Domains
{
    /// <summary>
    /// 事件主题容器
    /// </summary>
    public class EventTopicContainer
    {
        //TODO 在这里把各种事件划分好Topic,使用XXX.Infrastructure.Message.TopicInfo类划分

        public static MessageTopicInfo EmployeeEventTopic = new MessageTopicInfo(
            "EmployeeEventTopic", 
            typeof(EmployeeCreated), 
            typeof(EmployeePasswordChanged),
            typeof(EmployeeBaseInfoChanged),
            typeof(EmployeeRemoved),
            typeof(EmployeeStatusChanged)
            );

        public static MessageTopicInfo DepartmentEventTopic = new MessageTopicInfo(
            "DepartmentEventTopic",
            typeof(DepartmentCreated),
            typeof(DepartmentChildAdded),
            typeof(DepartmentChildRemoved),
            typeof(DepartmentRemoved),
            typeof(DepartmentPeopleAdded),
            typeof(DepartmentPeopleRemoved)
            );
    }


}
