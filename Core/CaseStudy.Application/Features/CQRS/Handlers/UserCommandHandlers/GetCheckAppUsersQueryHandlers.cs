using CaseStudy.Application.Features.CQRS.Queries.UserQueries;
using CaseStudy.Application.Features.CQRS.Results.UserResult;
using CaseStudy.Domian.Entites;
using CaseStudy.Domian.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Application.Features.CQRS.Handlers.UserCommandHandlers
{
    public class GetCheckAppUsersQueryHandlers
    {
        private readonly IProductRepository<User> _appUserRepository;

        public GetCheckAppUsersQueryHandlers( IProductRepository<User> appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }

        public async Task<GetCheckAppUserQueryResult> Handle(GetCheckAppUserQuery request, CancellationToken cancellationToken)
        {
            var values = new GetCheckAppUserQueryResult();
            var user = await _appUserRepository.GetByFilterAsync(x => x.PasswordHash == request.Password && x.Email == request.Mail);

            if (user != null)
            {
                values.Id = user.Id;
                values.Mail = user.Email;

            }
            return values;
        }
    }
}
