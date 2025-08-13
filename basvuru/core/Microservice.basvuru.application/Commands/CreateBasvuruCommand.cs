using MediatR;
using Microservice.basvuru.application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.basvuru.application.Commands
{
    public record CreateMusteriBasvuruCommand(MusteriBasvuruDto BasvuruDto) : IRequest<Guid>
    {
        public string? MusteriNo { get;  set; }
        public int? Basvurutipi { get; set; }
        public Guid MusteriBasvuru_UID { get; set; }
    }
}
