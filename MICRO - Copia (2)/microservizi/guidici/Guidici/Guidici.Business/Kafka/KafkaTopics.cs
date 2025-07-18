
namespace Guidici.Business.Kafka
{

    public abstract class AbstractKafkaTopics: IkafkaTopics
    {
        public const string SectionName = "Kafka:Topics";
        public abstract IEnumerable<string> GetTopics();
    }

    public class KafkaTopics : AbstractKafkaTopics
    {
        public string Votazione { get; set; } = "votazione";

        public override IEnumerable<string> GetTopics() => [Votazione];


    }
}
