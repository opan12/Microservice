using Microsoft.AspNetCore.Mvc;
using Microservice.basvuru.infrastructure.Data;
using Microservice.basvuru.domain.Entity;
using Microservice.basvuru.application.Commands;
using MediatR;
using Microservice.basvuru.application.DTO;
using Azure.Core;
using Microservice.basvuru.domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using RabbitMQ.Client;
using System.Text;
using Microservice.basvuru.api.Services;
using StackExchange.Redis;
using Newtonsoft.Json;
using Microservice.Shared.DTO;

namespace Microservice.basvuru.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasvuruController : ControllerBase
    {
        private readonly BasvuruDbContext _context;
        private readonly IDatabase _redisDb;

        public BasvuruController(BasvuruDbContext context)
        {
            var redis = ConnectionMultiplexer.Connect("localhost:6379");

            _context = context;
            _redisDb = redis.GetDatabase();


        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var list = _context.Basvurular.ToList();
            return Ok(list);
        }
        public class MusteriBasvuruRequest
        {
            public int BasvuruTipi  { get; set; } // Client’tan gelen tek alan
        }


        [HttpPost("musteriform")]
        public async Task<IActionResult> MusteriForm(
         [FromBody] MusteriBasvuruDto request,
         [FromHeader(Name = "Session-Id")] string sessionId) 
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

            _context.Basvurular.Add(basvuru);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Başvuru odu ve kuyruğa eklendi." });
        }
    }
}

