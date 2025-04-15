using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NaturAI.Model;

namespace NaturAI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [AllowAnonymous]
        [HttpPost("registrar")]
        public async Task<ActionResult> RegistrarUsuario([FromBody] UsuarioDTO novoUsuario)
        {
            var resultado = await _usuarioService.Registrar(novoUsuario);
            if (!resultado.Sucesso)
                return BadRequest(new { mensagem = resultado.Mensagem });

            return Ok(new { mensagem = resultado.Mensagem });
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> LoginUsuario([FromBody] LoginDTO login)
        {
            var resultado = await _usuarioService.Login(login);
            if (!resultado.Sucesso)
                return Unauthorized(new { mensagem = resultado.Mensagem });

            return Ok(new
            {
                mensagem = resultado.Mensagem,
                token = resultado.Token
            });
        }
    }
}
