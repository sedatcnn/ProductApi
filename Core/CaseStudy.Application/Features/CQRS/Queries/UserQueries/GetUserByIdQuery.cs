using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Application.Features.CQRS.Queries.UserQueries
{
    public class GetUserByIdQuery
    {
        public Guid Id { get; set; }
        public GetUserByIdQuery(Guid id) => Id = id;
    }
}
