using Microservice.basvuru.domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.basvuru.application.Abstract
{
    public interface IBasvuruRepository
    {
      Task AddBasvuruAsync(MusteriBasvuru basvuru);
       //Task<User> GetUserByMusteriNoAsync(string musteriNo);
    }
}
