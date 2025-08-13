using Microservice.basvuru.domain.Entity;

namespace Microservice.basvuru.insfrasture.Repository
{
    public interface IBasvuruRepository
    {
        Task AddAsync(MusteriBasvuru basvuru);
        Task<MusteriBasvuru> GetByIdAsync(Guid id);
    }
}