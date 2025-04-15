
namespace NaturAI.Repository
{
    public interface IProfessorRepository
    {
        Task<bool> VincularAlunoAoProfessor(string emailAluno, string emailProfessor);
    }
}