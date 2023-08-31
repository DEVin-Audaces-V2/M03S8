using RabbitMQ.Client;

namespace FichaCadastroRabbitMQ
{
    public interface IMessageRabbitMQ 
    {
        ConfigureRabbitMQ ConfigureRabbitMQ { get; set; }

        IModel ConfigureVirtualHost();
        void ExchangeDeclare();
        void QueueDeclare();
    }
}