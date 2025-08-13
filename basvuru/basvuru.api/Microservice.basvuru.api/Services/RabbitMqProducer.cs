using RabbitMQ.Client;
using System.Text;
using System.Text.Json;


namespace Microservice.basvuru.api.Services

{
    public class RabbitMqProducer

    {
        private readonly ConnectionFactory _factory;

        public RabbitMqProducer()
        {
            _factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
        }

        public async void SendMessage(object messageObj, string queueName)
        {
            var factory = new ConnectionFactory { HostName = "localhost" , UserName = "guest", Password = "guest"};
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();



            await channel.QueueDeclareAsync(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

           

            var json = JsonSerializer.Serialize(messageObj);
            var body = Encoding.UTF8.GetBytes(json);

            await channel.BasicPublishAsync(exchange: string.Empty, routingKey: queueName, body: body);



            //var factory = new ConnectionFactory
            //{
            //    HostName = "localhost",
            //    UserName = "guest",
            //    Password = "guest"
            //};
            //using var connection = factory.CreateConnectionAsync();
            //using var channel = connection.CreateModel();

            //channel.QueueDeclareAsync(queue: queueName,
            //                     durable: false,
            //                     exclusive: false,
            //                     autoDelete: false,
            //                     arguments: null);



            //channel.BasicPublish(exchange: "",
            //                     routingKey: queueName,
            //                     basicProperties: null,
            //                     body: body);
        }
    }

}