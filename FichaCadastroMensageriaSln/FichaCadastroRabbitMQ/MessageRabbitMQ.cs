using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace FichaCadastroRabbitMQ
{
    public class MessageRabbitMQ : IMessageRabbitMQ
    {
        private IModel _channel;
        private readonly IFactoryConnectionRabbitMQ _factoryConnectionRabbitMQ;

        public ConfigureRabbitMQ ConfigureRabbitMQ { get; set; }

        public MessageRabbitMQ(IFactoryConnectionRabbitMQ factoryConnectionRabbitMQ)
        {
            _factoryConnectionRabbitMQ = factoryConnectionRabbitMQ;
        }

        public IModel ConfigureVirtualHost()
        {
            _channel = _factoryConnectionRabbitMQ.CriarConexao(ConfigureRabbitMQ.VirtualHost);
            return _channel;
        }

        public void ExchangeDeclare()
        {
            _channel.ExchangeDeclare(ConfigureRabbitMQ.Exchange, ConfigureRabbitMQ.Type, ConfigureRabbitMQ.Durable, ConfigureRabbitMQ.AutoDelete);
        }

        public void QueueDeclare()
        {
            _channel.QueueDeclare(ConfigureRabbitMQ.Queue, durable: ConfigureRabbitMQ.Durable, false,  ConfigureRabbitMQ.AutoDelete);
         }


    }
}
