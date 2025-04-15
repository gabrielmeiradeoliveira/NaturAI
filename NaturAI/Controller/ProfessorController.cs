using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace NaturAI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProfessorController : ControllerBase
    {
        private readonly IProfessorService _professorService;

        public ProfessorController(IProfessorService professorService)
        {
            _professorService = professorService;
        }

        [HttpPost("adicionar-aluno")]
        public async Task<ActionResult> AdicionarAluno([FromBody] string emailAluno)
        {
            var permissaoClaim = User.Claims.FirstOrDefault(c => c.Type == "Permissao");
            var emailProfessor = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            if (permissaoClaim == null || !(permissaoClaim.Value == "Admin" || permissaoClaim.Value == "Professor"))
                return Forbid();

            if (string.IsNullOrEmpty(emailProfessor))
                return Unauthorized("Token inválido: e-mail não encontrado.");

            try
            {
                if (emailAluno.Equals(emailProfessor, StringComparison.OrdinalIgnoreCase))
                {
                    return BadRequest("O professor não pode ser vinculado como aluno.");
                }

                var sucesso = await _professorService.VincularAlunoAoProfessor(emailAluno, emailProfessor);

                if (!sucesso)
                    return Conflict("Esse aluno já está vinculado a esse professor.");

                return Ok($"Aluno com e-mail {emailAluno} vinculado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
