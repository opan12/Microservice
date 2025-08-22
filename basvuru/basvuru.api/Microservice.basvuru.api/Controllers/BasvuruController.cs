using Microsoft.AspNetCore.Mvc;
using Microservice.basvuru.application.DTO;
using Microservice.basvuru.domain.Entity;
using Microservice.basvuru.domain.Enum;
using Microservice.basvuru.api;
using Microservice.basvuru.api.Services;
using Microservice.Shared.DTO;
using StackExchange.Redis;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Microservice.basvuru.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasvuruController : ControllerBase
    {
        private readonly MongoDbContext _mongoContext;
        private readonly IDatabase _redisDb;
        private readonly ILogger<BasvuruController> _logger;

        public BasvuruController(MongoDbContext mongoContext, IConfiguration configuration, ILogger<BasvuruController> logger)
        {
            _mongoContext = mongoContext;
            _logger = logger;

            var redisHost = configuration["Redis:Host"];
            var redisPort = configuration["Redis:Port"];
            var redis = ConnectionMultiplexer.Connect($"{redisHost}:{redisPort},abortConnect=false");
            _redisDb = redis.GetDatabase();
        }

        [HttpPost("musteriform")]
        public async Task<IActionResult> MusteriForm(
            [FromBody] MusteriBasvuruDto request,
            [FromHeader(Name = "Session-Id")] string sessionId,
            [FromServices] RabbitMqProducer producer)
        {
            try
            {
                var userData = _redisDb.StringGet(sessionId);
                if (userData.IsNullOrEmpty)
                    return Unauthorized("Session geçersiz veya süresi dolmuş");

                var user = JsonConvert.DeserializeObject<UserSessionDto>(userData);

                var basvuru = new MusteriBasvuru
                {
                    MusteriBasvuru_UID = user.UserId,
                    Basvurutipi = request.Basvurutipi,
                    BasvuruTarihi = DateTime.Now,
                    BasvuruDurum = Durum.Beklemede,
                    Kayit_Durum = "Aktif",
                    HataAciklama = "",
                    MusteriNo = user.MusteriNo,
                    Kayit_Yapan = user.Username,
                    Kayit_Zaman = DateTime.Now
                };

                // MongoDB'ye ekleme
                await _mongoContext.Basvurular.InsertOneAsync(basvuru);

                // RabbitMQ kuyruğuna gönder
                producer.SendMessage(basvuru, "basvuru_queue");

                return Ok(new { message = "Başvuru okundu ve kuyruğa eklendi." });
            }
            catch (RedisConnectionException rex)
            {
                _logger.LogError(rex, "Redis bağlantı hatası.");
                return StatusCode(500, new { message = "Redis bağlantı hatası.", detail = rex.Message });
            }
            catch (RabbitMQ.Client.Exceptions.BrokerUnreachableException rbx)
            {
                _logger.LogError(rbx, "RabbitMQ bağlantı hatası.");
                return StatusCode(500, new { message = "RabbitMQ bağlantı hatası.", detail = rbx.Message });
            }
        }
    }
}