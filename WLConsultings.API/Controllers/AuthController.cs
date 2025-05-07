using Microsoft.AspNetCore.Mvc;
using WLConsultings.Application.Interfaces;
using WLConsultings.Application.Dtos;
using System.Threading.Tasks;

namespace WLConsultings.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // Endpoint de registro de usuário
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest model)
        {
            if (model == null || string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
                return BadRequest("Dados inválidos.");
            }

            try
            {
                await _authService.RegisterAsync(model.Name, model.Email, model.Password);
                return Ok("Usuário registrado com sucesso.");
            }
            catch (System.Exception ex)
            {
                return BadRequest($"Erro ao registrar usuário: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            if (model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
                return BadRequest("Dados inválidos.");
            }

            try
            {
                var token = await _authService.AuthenticateAsync(model.Email, model.Password);
                return Ok(new { Token = token });
            }
            catch (System.Exception ex)
            {
                return Unauthorized($"Erro ao autenticar: {ex.Message}");
            }
        }
    }
}
