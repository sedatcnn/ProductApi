using CaseStudy.Application.Features.CQRS.Commands.ProductCommand;
using CaseStudy.Application.Features.CQRS.Handlers.ProductCommandHandlers;
using CaseStudy.Application.Features.CQRS.Queries.ProductQueries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("global")]
    public class ProductsController : ControllerBase
    {
        private readonly CreateProductCommandHandler _createHandler;
        private readonly UpdateProductCommandHandler _updateHandler;
        private readonly DeleteProductCommandHandler _deleteHandler;
        private readonly GetAllProductsQueryHandler _getAllHandler;
        private readonly GetProductByIdQueryHandler _getByIdHandler;

        public ProductsController(
            CreateProductCommandHandler createHandler,
            UpdateProductCommandHandler updateHandler,
            DeleteProductCommandHandler deleteHandler,
            GetAllProductsQueryHandler getAllHandler,
            GetProductByIdQueryHandler getByIdHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _deleteHandler = deleteHandler;
            _getAllHandler = getAllHandler;
            _getByIdHandler = getByIdHandler;
        }

        [HttpGet("api/getAllProduct")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _getAllHandler.Handle(new GetAllProductsQuery());
            return Ok(result);
        }

        [HttpGet("api/getProduct/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _getByIdHandler.Handle(new GetProductByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost("api/creat")]
        public async Task<IActionResult> Create(CreateProductCommand command)
        {
            await _createHandler.Handle(command);
            return Ok("Product created successfully");
        }

        [HttpPut("api/update/{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateProductCommand command)
        {
            command.Id = id;
            await _updateHandler.Handle(command);
            return Ok("Product updated successfully");
        }

        [HttpDelete("api/delete{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _deleteHandler.Handle(new DeleteProductCommand(id));
            return Ok("Product deleted successfully");
        }
    }
}
