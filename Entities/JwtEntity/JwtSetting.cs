using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.JwtEntity
{
    public class JwtSetting
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecretKey { get; set; }
        public int ExpirationMinutes { get; set; }
    }
}
