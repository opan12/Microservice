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

namespace Microservice.basvuru.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasvuruController : ControllerBase
    {
        private readonly BasvuruDbContext _context;

        public BasvuruController(BasvuruDbContext context)
        {
            _context = context;

        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var list = _context.Basvurular.ToList();
            return Ok(list);
        }
       
        [HttpPost("musteriform")]
        public async Task<IActionResult> MusteriForm([FromBody] MusteriBasvuruDto dto, [FromServices] RabbitMqProducer producer)
        {
            var basvuru = new MusteriBasvuru
            {
                MusteriBasvuru_UID = dto.MusteriBasvuru_UID,
                Basvurutipi = dto.Basvurutipi,
                BasvuruTarihi = DateTime.Now,
                BasvuruDurum = Durum.Beklemede,
                Kayit_Durum = "Aktif",
                HataAciklama="",
                MusteriNo = dto.MusteriNo ?? "Unknown",
                Kayit_Yapan = User.Identity?.Name ?? "Unknown",
                Kayit_Zaman = DateTime.Now
            };

            _context.Basvurular.Add(basvuru);
            await _context.SaveChangesAsync();

            producer.SendMessage(basvuru, "basvuru_queue");

            return Ok(new { message = "Başvuru oluşturuldu ve kuyruğa eklendi." });
        }

    }
}