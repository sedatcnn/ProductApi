using CaseStudy.Application.DTOs;
using CaseStudy.Application.Features.CQRS.Handlers.UserCommandHandlers;
using CaseStudy.Application.Features.CQRS.Queries.UserQueries;
using CaseStudy.Application.Tools;
using CaseStudy.Domian.Entites;
using CaseStudy.Domian.Interfaces;
using CaseStudy.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly GetCheckAppUsersQueryHandlers _handler;
        private readonly TokenService _tokenService;
        private readonly IProductRepository<User> _userRepository;

        public LoginController(GetCheckAppUsersQueryHandlers handler, TokenService tokenService, IProductRepository<User> userRepository)
        {
            _handler = handler;
            _tokenService = tokenService;
            _userRepository = userRepository;

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(GetCheckAppUserQuery query)
        {
            var userResult = await _handler.Handle(query, default);

            if (userResult == null || userResult.Id == Guid.Empty)
                return Unauthorized("Geçersiz kullanıcı adı veya şifre");

            // User objesini veritabanından çekiyoruz
            var user = await _userRepository.GetByIdAsync(userResult.Id);
            if (user == null)
                return Unauthorized();

            // TokenService'e user gönderiyoruz
            var (jwtToken, refreshToken) = await _tokenService.GenerateTokensAsync(user);

            return Ok(new
            {
                Token = jwtToken,
                RefreshToken = refreshToken
            });
        }


        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest request)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
                return Unauthorized();

            var isValid = await _tokenService.ValidateRefreshTokenAsync(user.Id, request.RefreshToken);
            if (!isValid)
                return Unauthorized();

            var (jwtToken, newRefreshToken) = await _tokenService.GenerateTokensAsync(user);

            await _tokenService.RevokeRefreshTokenAsync(request.RefreshToken);

            return Ok(new
            {
                Token = jwtToken,
                RefreshToken = newRefreshToken
            });
        }
    }
}