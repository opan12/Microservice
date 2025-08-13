using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.basvuru.application.DTO
{
    public class UserDto
    {
        public string MusteriNo { get; set; }
        public string Id { get; set; }  
        public string Username { get; set; }
        public DateTime Yas { get; set; }
        public string Email { get; set; }
    }

}
