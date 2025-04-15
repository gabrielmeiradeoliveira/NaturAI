
public interface IProfessorService
{
    Task<bool> VincularAlunoAoProfessor(string emailAluno, string emailProfessor);
}