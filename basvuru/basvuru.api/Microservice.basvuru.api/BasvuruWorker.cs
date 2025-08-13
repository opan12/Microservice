using Microservice.basvuru.application.DTO;
using Microservice.basvuru.domain.Enum;
using Microservice.basvuru.infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

namespace Microservice.basvuru.api
{
    public class BasvuruWorker : BackgroundService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IServiceScopeFactory _scopeFactory;

        public BasvuruWorker(IHttpClientFactory httpClientFactory, IServiceScopeFactory scopeFactory)
        {
            _httpClientFactory = httpClientFactory;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var client = _httpClientFactory.CreateClient();
                var users = await client.GetFromJsonAsync<List<UserDto>>(
                    "http://localhost:5027/api/User/kullanicilar", stoppingToken);
                Console.WriteLine($"Kullanıcı sayısı: {users?.Count ?? 0}");

                using (var scope = _scopeFactory.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<BasvuruDbContext>();

                    var basvurular = db.Basvurular.ToList();
                    var tarih = DateTime.Now.Year;
                    foreach (var basvuru in basvurular)
                    {
                        var user = users.FirstOrDefault(u => u.MusteriNo == basvuru.MusteriNo);
                        if (user != null && tarih - user.Yas.Year >= 18)
                        {
                            basvuru.BasvuruDurum = 0;
                            basvuru.HataAciklama = "sağlandı";
                        }
                        else
                        {
                            basvuru.BasvuruDurum = (Durum)1;
                            basvuru.HataAciklama = "Kullanıcı bilgisi bulunamadı veya yaş kriteri sağlanmadı.";
                        }
                    }

                    await db.SaveChangesAsync(stoppingToken);
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
