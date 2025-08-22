using CaseStudy.Application.Features.CQRS.Queries.ProductQueries;
using CaseStudy.Application.Features.CQRS.Results.ProductResult;
using CaseStudy.Domian.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Application.Features.CQRS.Handlers.ProductCommandHandlers
{
    public class GetAllProductsQueryHandler
    {
        private readonly IProductService _productService; // Cache ile çalışan servis

        public GetAllProductsQueryHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<List<GetAllProductsQueryResult>> Handle(GetAllProductsQuery query)
        {
            var products = await _productService.GetAllAsync(); // artık cache kullanıyor

            return products.Select(p => new GetAllProductsQueryResult
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock
            }).ToList();
        }
    }

}
