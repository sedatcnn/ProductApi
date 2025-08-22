using CaseStudy.Application.Features.CQRS.Queries.ProductQueries;
using CaseStudy.Application.Features.CQRS.Results.ProductResult;
using CaseStudy.Domian.Entites;
using CaseStudy.Domian.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Application.Features.CQRS.Handlers.ProductCommandHandlers
{
    public class GetProductByIdQueryHandler
    {
        private readonly IProductRepository<Product> _repository;
        public GetProductByIdQueryHandler(IProductRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<GetProductByIdQueryResult> Handle(GetProductByIdQuery query)
        {
            var product = await _repository.GetByIdAsync(query.Id);
            if (product == null) return null;

            return new GetProductByIdQueryResult
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock
            };
        }
    }
}
