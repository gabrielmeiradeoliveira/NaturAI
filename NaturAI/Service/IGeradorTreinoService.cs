using NaturAI.Model;

namespace NaturAI.Service
{
    public interface IGeradorTreinoService
    {
        Task<TreinoDTO> GerarETreinar(int usuarioId);
    }
}