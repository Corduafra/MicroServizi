namespace Registrazioni.Business.kafka.kafkaAbstract
{
    public interface IkafkaTopics
    {
        IEnumerable<string> GetTopics();
    }
}
