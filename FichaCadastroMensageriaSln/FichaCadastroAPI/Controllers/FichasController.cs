using FichaCadastroRabbitMQ;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;

namespace FichaCadastroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FichasController : ControllerBase
    {
        private readonly IMessageRabbitMQ _messageRabbitMQ;

        public FichasController(IMessageRabbitMQ messageRabbitMQ)
        {
            _messageRabbitMQ = messageRabbitMQ;
        }

        [HttpPost]
        [Route("cadastro/novo/topic")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult CadastroNovoTopicPost()
        {
            try
            {
                _messageRabbitMQ.ConfigureRabbitMQ = new ConfigureRabbitMQ(
                    VirtualHost: "ficha",
                    Exchange: "ficha-exchange-topic",
                    Type: "topic",
                    Queue: "ficha-cadastro-novo-queue-topic",
                    RouteKey: "ficha-cadastro.novo-routeKey-topic"
                );

                _messageRabbitMQ.ConfigureVirtualHost();
                _messageRabbitMQ.ExchangeDeclare();
                _messageRabbitMQ.QueueDeclare();
                _messageRabbitMQ.QueueBind();

                int id = 1;

                /*CUIDADO AO MEXER NESSA LINHA*/
                while (id <= 10000)
                {
                    _messageRabbitMQ.ConfigureRabbitMQ.Message = Encoding.UTF8.GetBytes($"Id {id}  Data e Hora da aplicação {DateTime.Now}");
                    _messageRabbitMQ.BasicPublish();
                    id++;
                }

                return StatusCode(HttpStatusCode.Created.GetHashCode());
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex);
            }
        }
    }



}
