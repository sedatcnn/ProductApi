using CaseStudy.Application.Features.CQRS.Queries.UserQueries;
using CaseStudy.Application.Features.CQRS.Results.UserResult;
using CaseStudy.Domian.Entites;
using CaseStudy.Domian.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Application.Features.CQRS.Handlers.UserCommandHandlers
{
    public class GetUserByIdQueryHandler
    {
        private readonly IProductRepository<User> _repo;
        public GetUserByIdQueryHandler(IProductRepository<User> repo) => _repo = repo;

        public async Task<GetUserResult?> Handle(GetUserByIdQuery query)
        {
            var user = await _repo.GetByIdAsync(query.Id);
            if (user == null) return null;

            return new GetUserResult
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
            };
        }
    }
}
