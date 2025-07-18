using Confluent.Kafka;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KafkaConsoleChat
{
    class Program
    {
        private static readonly string _bootstrapServers = "localhost:9092";
        private static readonly string _topic = "chat-message";
        private static CancellationTokenSource _cancellationTokenSource;

        static async Task Main(string[] args)
        {
            Console.WriteLine("Kafka Console Chat Application");
            Console.Write("Enter your username: ");
            string username = Console.ReadLine() ?? "Anonymous";

            // Start the consumer in a separate task
            _cancellationTokenSource = new CancellationTokenSource();
            var consumerTask = StartConsumerAsync(_cancellationTokenSource.Token);

            Console.WriteLine("Type your messages and press Enter to send. Type 'exit' to quit.");

            // Producer loop
            while (true)
            {
                string? message = Console.ReadLine();
                if (string.IsNullOrEmpty(message))
                    continue;

                if (message.ToLower() == "exit")
                    break;

                await SendMessageAsync(username, message);
            }

            // Clean up
            _cancellationTokenSource.Cancel();
            try
            {
                await consumerTask;
            }
            catch (OperationCanceledException)
            {
                // Expected when cancellation token is used
            }
        }

        private static async Task SendMessageAsync(string username, string message)
        {
            try
            {
                var config = new ProducerConfig
                {
                    BootstrapServers = _bootstrapServers
                };

                using (var producer = new ProducerBuilder<Null, string>(config).Build())
                {
                    string formattedMessage = $"{username}: {message}";
                    var result = await producer.ProduceAsync(_topic, new Message<Null, string> { Value = formattedMessage });
                    producer.Flush(TimeSpan.FromSeconds(10));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message: {ex.Message}");
            }
        }

        private static async Task StartConsumerAsync(CancellationToken cancellationToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = _bootstrapServers,
                GroupId = Guid.NewGuid().ToString(),
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                consumer.Subscribe(_topic);

                try
                {
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        try
                        {
                            var consumeResult = consumer.Consume(cancellationToken);
                            if (consumeResult != null)
                            {
                                // Display the message but don't show our own messages again
                                Console.WriteLine(consumeResult.Message.Value);
                            }
                        }
                        catch (ConsumeException ex)
                        {
                            Console.WriteLine($"Consume error: {ex.Message}");
                        }
                    }
                }
                finally
                {
                    consumer.Close();
                }
            }
        }
    }
}