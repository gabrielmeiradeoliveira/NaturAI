using NaturAI.Repository;

public class ProfessorService : IProfessorService
{
    private readonly IProfessorRepository _professorRepository;
    private readonly IUsuarioRepository _usuarioRepository;

    public ProfessorService(IProfessorRepository professorRepository, IUsuarioRepository usuarioRepository)
    {
        _professorRepository = professorRepository;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<bool> VincularAlunoAoProfessor(string emailAluno, string emailProfessor)
    {
        var alunoExiste = await _usuarioRepository.ExistePorEmail(emailAluno);

        if (!alunoExiste)
            throw new Exception("Aluno não encontrado.");

        if (emailAluno.Equals(emailProfessor, StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception("O professor não pode ser vinculado como aluno.");
        }

        var sucesso = await _professorRepository.VincularAlunoAoProfessor(emailAluno, emailProfessor);

        if (!sucesso)
        {
            throw new Exception("Este aluno já está vinculado a este professor.");
        }

        return sucesso;
    }
}
