using CaseStudy.Application.Features.CQRS.Commands.ProductCommand;
using CaseStudy.Domian.Entites;
using CaseStudy.Domian.Interfaces;


namespace CaseStudy.Application.Features.CQRS.Handlers.ProductCommandHandlers
{
    public class CreateProductCommandHandler
    {
        private readonly IProductRepository<Product> _repository;
        public CreateProductCommandHandler(IProductRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateProductCommand command)
        {
            var product = new Product
            {
                Name = command.Name,
                Price = command.Price,
                Stock = command.Stock
            };
            await _repository.CreatAsync(product);
        }
    }
}
