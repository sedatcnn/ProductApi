using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Application.DTOs
{
    public class RefreshRequest
    {
        public Guid UserId { get; set; }
        public string RefreshToken { get; set; } = null!;
    }
}
