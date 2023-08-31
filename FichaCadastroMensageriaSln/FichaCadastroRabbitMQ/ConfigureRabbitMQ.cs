namespace FichaCadastroRabbitMQ
{
    public record ConfigureRabbitMQ(string VirtualHost,
                                    string Exchange,  
                                    string Type, 
                                    string Queue, 
                                    bool AutoDelete = false, 
                                    bool Durable = true);
}
