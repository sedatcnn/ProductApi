using CaseStudy.Application.Features.CQRS.Queries.UserQueries;
using CaseStudy.Application.Features.CQRS.Results.UserResult;
using CaseStudy.Domian.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Application.Features.CQRS.Handlers.UserCommandHandlers
{
    public class GetAllUsersQueryHandler
    {
        private readonly IWithRepository _withRepo;
        public GetAllUsersQueryHandler(IWithRepository withRepo) => _withRepo = withRepo;

        public async Task<List<GetUserResult>> Handle(GetAllUsersQuery query)
        {
            var users =  _withRepo.GetUserAsync();
            return users.Select(u => new GetUserResult
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
            }).ToList();
        }
    }
}
