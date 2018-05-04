using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Google.Infrastructure.Message
{
    public class MessageTopicInfo
    {
        public string TopicName { get; private set; }

        public Type[] MessageTypes { get; private set; }

        public MessageTopicInfo(string topicName, params Type[] messageTypes)
        {
            TopicName = topicName;
            MessageTypes = messageTypes;
        }
    }
}
