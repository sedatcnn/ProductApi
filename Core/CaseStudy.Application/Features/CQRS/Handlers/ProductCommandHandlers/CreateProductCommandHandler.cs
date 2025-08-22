using CaseStudy.Application.Features.CQRS.Commands.ProductCommand;
using CaseStudy.Domian.Entites;
using CaseStudy.Domian.Interfaces;


namespace CaseStudy.Application.Features.CQRS.Handlers.ProductCommandHandlers
{
    public class CreateProductCommandHandler
    {
        private readonly IProductRepository<Product> _repository;
        private readonly IEventBus _eventBus; // Bu satırı ekle

        public CreateProductCommandHandler(IProductRepository<Product> repository, IEventBus eventBus) // Constructor'ı güncelle
        {
            _repository = repository;
            _eventBus = eventBus;
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
            _eventBus.Publish(new { ProductId = product.Id, ProductName = product.Name, Action = "ProductCreated" });

        }
    }
}
