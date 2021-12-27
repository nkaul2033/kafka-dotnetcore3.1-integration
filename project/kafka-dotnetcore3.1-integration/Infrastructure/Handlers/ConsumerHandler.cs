namespace Infrastructure.Handlers
{
    using Confluent.Kafka;
    using System;
    using System.Threading;
    public class ConsumerHandler
    {
        private string _topicName;
        private ConsumerConfig _consumerConfig;
        private Consumer<string, string> _consumer;
        private static readonly Random rand = new Random();
        public ConsumerHandler(ConsumerConfig config, string topicName)
        {
            _topicName = topicName;
            _consumerConfig = config;
            _consumer = new Consumer<string, string>(_consumerConfig);
            _consumer.Subscribe(topicName);
        }
        public string readMessage()
        {
            var consumeResult = _consumer.Consume();
            return consumeResult.Value;
        }
    }
}