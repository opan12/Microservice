using Microsoft.EntityFrameworkCore;
using Microservice.basvuru.domain.Entity;

namespace Microservice.basvuru.infrastructure.Data
{
    public class BasvuruDbContext : DbContext
    {
        public BasvuruDbContext(DbContextOptions<BasvuruDbContext> options) : base(options) { }

        public DbSet<MusteriBasvuru> Basvurular { get; set; }
    }
}
