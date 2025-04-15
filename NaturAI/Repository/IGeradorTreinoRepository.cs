using NaturAI.Model;

namespace NaturAI.Repository
{
    public interface IGeradorTreinoRepository
    {
        Task SalvarTreino(TreinoDTO treino);
    }
}