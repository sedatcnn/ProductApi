using CaseStudy.Application.Features.CQRS.Commands.ProductCommand;
using CaseStudy.Domian.Entites;
using CaseStudy.Domian.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Application.Features.CQRS.Handlers.ProductCommandHandlers
{
    public class DeleteProductCommandHandler
    {
        private readonly IProductRepository<Product> _repository;
        public DeleteProductCommandHandler(IProductRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteProductCommand command)
        {
            var product = await _repository.GetByIdAsync(command.Id);
            if (product != null)
            {
                await _repository.RemoveAsync(product);
            }
        }
    }
}
