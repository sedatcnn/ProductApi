using CaseStudy.Application.Features.CQRS.Handlers.UserCommandHandlers;
using CaseStudy.Application.Features.CQRS.Queries.UserQueries;
using CaseStudy.Application.Tools;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly GetCheckAppUsersQueryHandlers _handler;

        public LoginController(GetCheckAppUsersQueryHandlers handler)
        {
            _handler = handler;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(GetCheckAppUserQuery query)
        {
            var userResult = await _handler.Handle(query, default);

            if (userResult == null || userResult.Id == Guid.Empty)
                return Unauthorized("Geçersiz kullanıcı adı veya şifre");

            var token = JwtTokenGenerateToken.GenerateToken(userResult);
            return Ok(token);
        }
    }
}
