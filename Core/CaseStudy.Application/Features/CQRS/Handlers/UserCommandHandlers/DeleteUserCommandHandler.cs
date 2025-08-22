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
    public class DeleteUserCommandHandler
    {
        private readonly IProductRepository<User> _repo;
        public DeleteUserCommandHandler(IProductRepository<User> repo) => _repo = repo;

        public async Task Handle(DeleteUserCommand command)
        {
            var user = await _repo.GetByIdAsync(command.UserId);
            if (user != null)
            {
                await _repo.RemoveAsync(user);
            }
        }
    }
}
