using CaseStudy.Application.Features.CQRS.Commands.UserCommand;
using CaseStudy.Application.Features.CQRS.Handlers.UserCommandHandlers;
using CaseStudy.Application.Features.CQRS.Queries.UserQueries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly CreateUserCommandHandler _createHandler;
        private readonly UpdateUserCommandHandler _updateHandler;
        private readonly DeleteUserCommandHandler _deleteHandler;
        private readonly GetAllUsersQueryHandler _getAllHandler;
        private readonly GetUserByIdQueryHandler _getByIdHandler;

        public UsersController(
            CreateUserCommandHandler createHandler,
            UpdateUserCommandHandler updateHandler,
            DeleteUserCommandHandler deleteHandler,
            GetAllUsersQueryHandler getAllHandler,
            GetUserByIdQueryHandler getByIdHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _deleteHandler = deleteHandler;
            _getAllHandler = getAllHandler;
            _getByIdHandler = getByIdHandler;
        }

        // 🔹 Tüm kullanıcıları getir
        [HttpGet("api/allUser")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _getAllHandler.Handle(new GetAllUsersQuery());
            return Ok(result);
        }

        // 🔹 ID ile kullanıcı getir
        [HttpGet("api/getUser/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _getByIdHandler.Handle(new GetUserByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        // 🔹 Yeni kullanıcı oluştur
        [HttpPost("api/creat")]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            await _createHandler.Handle(command);
            return Ok("Kullanıcı oluşturuldu ✅");
        }

        // 🔹 Kullanıcı güncelle
        [HttpPut("api/update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand command)
        {
            await _updateHandler.Handle(command);
            return Ok("Kullanıcı güncellendi ✅");
        }

        // 🔹 Kullanıcı sil
        [HttpDelete("api/delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _deleteHandler.Handle(new DeleteUserCommand(id));
            return Ok("Kullanıcı silindi ❌");
        }
    }
}
