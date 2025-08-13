using Microservice.basvuru.domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.basvuru.domain.Entity
{
    public class MusteriBasvuru
    {
        [Key]
        public Guid Basvuru_UID { get; set; }
        public string? MusteriBasvuru_UID { get; set; }

        public string MusteriNo { get; set; }

        public Durum BasvuruDurum { get; set; }

        public Tip Basvurutipi { get; set; }
        public DateTime BasvuruTarihi { get; set; }
        public string HataAciklama { get; set; }
        public DateTime Kayit_Zaman { get; set; }
        public string Kayit_Yapan { get; set; }
        public string Kayit_Durum { get; set; }

    }
}
