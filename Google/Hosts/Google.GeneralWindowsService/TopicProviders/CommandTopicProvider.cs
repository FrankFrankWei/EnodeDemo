using ECommon.Components;
using ENode.Commanding;
using ENode.EQueue;
using OrganizationBC.Commands;

namespace Google.GeneralWindowsService.TopicProviders
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
