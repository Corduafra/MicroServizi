using Registrazioni.Business.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registrazioni.Business.kafka
{
    public class KafkaOptions
    {
        public const string SectionName = "Kafka";
        public bool Enable { get; set; } = true;
    }

    public abstract class KafkaClientOptions
    {
        public string BootstrapServers { get; set; } = string.Empty;

    }

 
    public class KafkaSettings : KafkaClientOptions
    {
        public const string SectionName = "Kafka:ConsumerClient";

        public string GroupId { get; set; } = string.Empty;
    }


    public interface IKafkaTopics
    {
        IEnumerable<string> GetTopics();
    }

    public abstract class AbstractKafkaTopics : IKafkaTopics
    {
        public const string SectionName = "Kafka:Topics";

        public abstract IEnumerable<string> GetTopics();
    }



}

