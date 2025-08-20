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
    public class CreateUserCommandHandler
    {
        private readonly IProductRepository<User> _repo;
        public CreateUserCommandHandler(IProductRepository<User> repo) => _repo = repo;

        public async Task Handle(CreateUserCommand command)
        {
            var user = new User
            {
                UserName = command.Username,
                Email = command.Email,
                PasswordHash = command.Password, 
            };
            await _repo.CreatAsync(user);
        }
    }
}
