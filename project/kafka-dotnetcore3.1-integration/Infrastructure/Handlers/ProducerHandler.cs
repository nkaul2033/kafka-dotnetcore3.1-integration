namespace Infrastructure.Handlers
{
    using Confluent.Kafka;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class ProducerHandler
    {
        private string _topicName;
        private Producer<string, string> _producer;
        private ProducerConfig _config;
        private static readonly Random rand = new Random();

        public ProducerHandler(ProducerConfig config, string topicName)
        {
            _topicName = topicName;
            _config = config;
            _producer = new Producer<string, string>(_config);
            _producer.OnError += (_, e) =>
            {
                Console.WriteLine("Exception:" + e);
            };
        }
        public async Task writeMessage(string message)
        {
            var dr = await _producer.ProduceAsync(_topicName, new Message<string, string>()
            {
                Key = rand.Next(5).ToString(),
                Value = message
            });
            Console.WriteLine($"KAFKA => Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
            return;
        }
    }
}