using FichaCadastroRabbitMQ;
using RabbitMQ.Client.Events;
using System.Text;

namespace FichaCadastroWorkService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IMessageRabbitMQ _messageRabbitMQ;


    public Worker(ILogger<Worker> logger, IMessageRabbitMQ messageRabbitMQ)
    {
        _logger = logger;
        _messageRabbitMQ = messageRabbitMQ;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

        _messageRabbitMQ.ConfigureRabbitMQ = new ConfigureRabbitMQ(
            VirtualHost: "ficha",
            Exchange: "ficha-exchange-topic",
            Type: "topic",
            Queue: "ficha-cadastro-novo-queue-topic",
            RouteKey: "ficha-cadastro.novo-routeKey-topic"
        );

        _messageRabbitMQ.ConfigureVirtualHost();

        var basicConsumer = _messageRabbitMQ.InstanciarEventingBasicConsumer();

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                basicConsumer.Received += (model, basicDeliverEventArgs) =>
                {
                    basicDeliverEventArgs.RoutingKey = _messageRabbitMQ.ConfigureRabbitMQ.RouteKey;
                    basicDeliverEventArgs.Exchange = _messageRabbitMQ.ConfigureRabbitMQ.Exchange;
                    var body = basicDeliverEventArgs.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    Console.WriteLine($"{message}");
                };

                //await Task.Delay(1000, stoppingToken);
            }
            catch (Exception ex)
            {

                //throw;
            }

            _messageRabbitMQ.BasicConsume(basicConsumer);

        }

    }
}
