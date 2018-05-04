using ECommon.Components;
using ENode.Commanding;
using ENode.EQueue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Infrastructure;
using Google.Infrastructure.Configs;
using OrganizationBC.Commands;

namespace OrganizationBC.EventService
{
    [Component]
    public class CommandTopicProvider : AbstractTopicProvider<ICommand>
    {
        public CommandTopicProvider()
        {
            //TODO 注册命令消息Topic
            RegisterTopic(
                CommandTopicContainer.EmployeeCommandTopic.TopicName,
                CommandTopicContainer.EmployeeCommandTopic.MessageTypes);
            RegisterTopic(
               CommandTopicContainer.DepartmentCommandTopic.TopicName,
               CommandTopicContainer.DepartmentCommandTopic.MessageTypes);
        }


    }
}
