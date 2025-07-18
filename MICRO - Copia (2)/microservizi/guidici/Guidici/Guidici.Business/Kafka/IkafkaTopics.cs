using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guidici.Business.Kafka
{
    internal interface IkafkaTopics
    {
        IEnumerable<string> GetTopics();
    }
}
