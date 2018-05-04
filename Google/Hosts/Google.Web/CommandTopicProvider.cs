using ECommon.Components;
using ENode.Commanding;
using ENode.EQueue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Infrastructure.Configs;
using OrganizationBC.Commands;

namespace Google.Web
{
    [Component]
    public class CommandTopicProvider : AbstractTopicProvider<ICommand>
    {
        public CommandTopicProvider()
        {
            //设置Web项目要发送的命令的Topic
            RegisterTopic(
                CommandTopicContainer.EmployeeCommandTopic.TopicName, 
                CommandTopicContainer.EmployeeCommandTopic.MessageTypes);
            RegisterTopic(
                CommandTopicContainer.DepartmentCommandTopic.TopicName,
                CommandTopicContainer.DepartmentCommandTopic.MessageTypes);
        }
    }
}
