using MediatR;
using Microservice.basvuru.application.Commands;
using Microservice.basvuru.domain.Entity;
using Microservice.basvuru.domain.Enum;
using Microservice.basvuru.application.Abstract;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

public class CreateMusteriBasvuruCommandHandler : IRequestHandler<CreateMusteriBasvuruCommand, Guid>
{
    private readonly IBasvuruRepository _repository;

    public CreateMusteriBasvuruCommandHandler(IBasvuruRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateMusteriBasvuruCommand request, CancellationToken cancellationToken)
    {
        var basvuru = new MusteriBasvuru
        {
            MusteriBasvuru_UID = "request.BasvuruDto.MusteriBasvuru_UID",  
            Basvuru_UID = Guid.NewGuid(), 
            MusteriNo = "request.BasvuruDto.MusteriNo",
            BasvuruDurum = Durum.Beklemede,
            Basvurutipi = request.BasvuruDto.Basvurutipi,
            BasvuruTarihi = DateTime.Now,
            HataAciklama = string.Empty,
            Kayit_Zaman = DateTime.Now,
            Kayit_Yapan = "User.Equals",
            Kayit_Durum = "Aktif"
        };

        await _repository.AddBasvuruAsync(basvuru);

        return basvuru.Basvuru_UID;
    }

}
