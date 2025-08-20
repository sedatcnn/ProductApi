using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Application.Tools
{
    public class JwtTokenDefaults
    {
        public const string ValidAudience = "https//localhost";
        public const string ValidIssuer = "https//localhost";
        public const string Key = "Case352354**Case352354**JWTSecretKey123!"; // Daha güçlü bir key (en az 16+ karakter)
        public const int Expire = 60;
    }
}
