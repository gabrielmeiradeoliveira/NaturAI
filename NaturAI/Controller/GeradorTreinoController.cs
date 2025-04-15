using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NaturAI.Service;

namespace NaturAI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class GeradorTreinoController : ControllerBase
    {
        private readonly IGeradorTreinoService _geradorTreinoService;

        public GeradorTreinoController(IGeradorTreinoService geradorTreinoService)
        {
            _geradorTreinoService = geradorTreinoService;
        }

        [HttpPost("gerar-treino")]
        public async Task<IActionResult> GerarTreino()
        {
            var usuarioIdClaim = User.FindFirst("id")?.Value;
            if (usuarioIdClaim == null || !int.TryParse(usuarioIdClaim, out var usuarioId))
                return Unauthorized("Usuário não identificado.");

            var treino = await _geradorTreinoService.GerarETreinar(usuarioId);
            return Ok(treino);
        }
    }
}
