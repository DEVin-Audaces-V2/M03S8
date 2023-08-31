using RabbitMQ.Client;

namespace FichaCadastroRabbitMQ
{
    /// <summary>
    /// Utilizando AddSingleton
    /// </summary>
    public class FactoryConnectionRabbitMQ : IFactoryConnectionRabbitMQ
    {
        private  IConnection _connection;
        private  IModel _channel;
        private readonly ConnectionFactory _connectionFactory;

        public FactoryConnectionRabbitMQ()
        {
            _connectionFactory = new ConnectionFactory()
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672/")
            };
        }

        public IModel CriarConexao(string virtualHost)
        {
            /*--
             * Validação para saber se as propriedades estao carregadas
             */

            //Recebe o virual host informado
            _connectionFactory.VirtualHost = virtualHost;
            
            ///Connection com o RabbitMQ
            _connection = _connectionFactory.CreateConnection();

            //Channel Das conexões
            _channel = _connection.CreateModel();
                
            return _channel;
        }
    }
}
