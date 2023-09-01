using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace FichaCadastroRabbitMQ
{
    public interface IMessageRabbitMQ 
    {
        ConfigureRabbitMQ ConfigureRabbitMQ { get; set; }

        IModel ConfigureVirtualHost();
        void ExchangeDeclare();
        void QueueDeclare();
        void QueueBind();
        void BasicPublish();
        void BasicConsume(IBasicConsumer basicConsumer);
        EventingBasicConsumer InstanciarEventingBasicConsumer();
    }
}