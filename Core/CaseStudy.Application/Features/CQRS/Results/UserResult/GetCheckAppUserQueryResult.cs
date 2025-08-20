using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Application.Features.CQRS.Results.UserResult
{
    public class GetCheckAppUserQueryResult
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Mail { get; set; }
    }
}
