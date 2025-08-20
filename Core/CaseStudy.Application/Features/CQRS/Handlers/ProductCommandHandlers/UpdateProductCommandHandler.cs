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
    public class UpdateProductCommandHandler
    {
        private readonly IProductRepository<Product> _repository;
        public UpdateProductCommandHandler(IProductRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateProductCommand command)
        {
            var product = await _repository.GetByIdAsync(command.Id);
            if (product != null)
            {
                product.Name = command.Name;
                product.Price = command.Price;
                product.Stock = command.Stock;
                await _repository.UpdateAsync(product);
            }
        }
    }
}
