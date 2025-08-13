using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using System;
using Microservice.basvuru.domain.Entity;

namespace Microservice.notification.Services
{
    public class BasvuruConsumer : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: "basvuru_queue",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    var basvuru = JsonSerializer.Deserialize<MusteriBasvuru>(message);

                    Console.WriteLine($"📥 Başvuru alındı: {basvuru?.MusteriBasvuru_UID} - {basvuru?.Basvurutipi}");

                   
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Mesaj işlenirken hata: {ex.Message}");
                }
            };

            await channel.BasicConsumeAsync(
                queue: "basvuru_queue",
                autoAck: true, 
                consumer: consumer);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}