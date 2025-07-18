using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Confluent.Kafka;

namespace KafkaChatApp
{
    public partial class Form1 : Form
    {
        private readonly string _bootstrapServers = "localhost:9092";
        private readonly string _topic = "chat-message";
        private CancellationTokenSource _cancellationTokenSource;
        private Task _consumerTask;

        public Form1()
        {
            InitializeComponent();
            this.FormClosing += Form1_FormClosing;
            StartConsumer();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Stop the consumer when the form is closing
            _cancellationTokenSource?.Cancel();
            try
            {
                _consumerTask?.Wait(TimeSpan.FromSeconds(5));
            }
            catch (Exception) { /* Ignore exceptions during shutdown */ }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMessage.Text))
                return;

            SendMessage(txtUsername.Text, txtMessage.Text);
            txtMessage.Clear();
        }

        private void SendMessage(string username, string message)
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
                    producer.Produce(_topic, new Message<Null, string> { Value = formattedMessage });
                    producer.Flush(TimeSpan.FromSeconds(10));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending message: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StartConsumer()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            var token = _cancellationTokenSource.Token;

            _consumerTask = Task.Run(() =>
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
                        while (!token.IsCancellationRequested)
                        {
                            var consumeResult = consumer.Consume(token);
                            if (consumeResult != null)
                            {
                                DisplayMessage(consumeResult.Message.Value);
                            }
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        // Expected when cancellation token is used
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error consuming messages: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        consumer.Close();
                    }
                }
            }, token);
        }

        private void DisplayMessage(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(DisplayMessage), message);
                return;
            }

            txtChatHistory.AppendText(message + Environment.NewLine);
            // Scroll to the bottom
            txtChatHistory.SelectionStart = txtChatHistory.Text.Length;
            txtChatHistory.ScrollToCaret();
        }
    }
}