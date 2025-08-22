using CaseStudy.Application.Features.CQRS.Commands.UserCommand;
using CaseStudy.Domian.Entites;
using CaseStudy.Domian.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Application.Features.CQRS.Handlers.UserCommandHandlers
{
    public class UpdateUserCommandHandler
    {
        private readonly IProductRepository<User> _repo;
        public UpdateUserCommandHandler(IProductRepository<User> repo) => _repo = repo;

        public async Task Handle(UpdateUserCommand command)
        {
            var user = await _repo.GetByIdAsync(command.Id);
            if (user == null) return;

            user.UserName = command.UserName;
            user.Email = command.Email;

            await _repo.UpdateAsync(user);
        }
    }
}
