using ECommon.Components;
using ENode.EQueue;
using ENode.Eventing;
using Google.Infrastructure.Configs;
using OrganizationBC.Domains;

namespace Google.GeneralWindowsService.TopicProviders
{
    [Component]
    public class EventTopicProvider : AbstractTopicProvider<IDomainEvent>
    {
        public EventTopicProvider()
        {
            RegisterTopic(EventTopicContainer.EmployeeEventTopic.TopicName, EventTopicContainer.EmployeeEventTopic.MessageTypes);
            RegisterTopic(EventTopicContainer.DepartmentEventTopic.TopicName, EventTopicContainer.DepartmentEventTopic.MessageTypes);
        }
    }
}
