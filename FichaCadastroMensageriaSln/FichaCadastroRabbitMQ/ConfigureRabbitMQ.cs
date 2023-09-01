using RabbitMQ.Client;

namespace FichaCadastroRabbitMQ
{
    public record ConfigureRabbitMQ(string VirtualHost,
                                    string Exchange,
                                    string Type,
                                    string Queue,
                                    string RouteKey,
                                    bool AutoDelete = false,
                                    bool Durable = true)
    {
        public byte[]? Message { get; set; }
    }
}
