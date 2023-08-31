using RabbitMQ.Client;

namespace FichaCadastroRabbitMQ
{
    public interface IFactoryConnectionRabbitMQ
    {
        IModel CriarConexao(string virtualHost);
    }

}
