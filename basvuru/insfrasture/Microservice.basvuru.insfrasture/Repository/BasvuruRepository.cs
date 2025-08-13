using Microservice.basvuru.domain.Entity;
using Microservice.basvuru.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.basvuru.insfrasture.Repository
{
    public class BasvuruRepository : IBasvuruRepository
    {
        private readonly BasvuruDbContext _context;

        public BasvuruRepository(BasvuruDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(MusteriBasvuru basvuru)
        {
            _context.Basvurular.Add(basvuru);
            await _context.SaveChangesAsync();
        }

        public Task<MusteriBasvuru> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        // Diğer repository metotları...
    }
}
