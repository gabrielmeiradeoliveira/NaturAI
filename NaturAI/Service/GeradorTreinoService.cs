using NaturAI.Model;
using NaturAI.Repository;

namespace NaturAI.Service
{
    public class GeradorTreinoService : IGeradorTreinoService
    {
        private readonly IGeradorTreinoRepository _treinoRepository;

        public GeradorTreinoService(IGeradorTreinoRepository treinoRepository)
        {
            _treinoRepository = treinoRepository;
        }

        public async Task<TreinoDTO> GerarETreinar(int usuarioId)
        {
            var treino = new TreinoDTO
            {
                UsuarioId = usuarioId,
                Descricao = "Treino gerado: Peito, Tríceps e Abdômen.",
                DataGeracao = DateTime.UtcNow,
                ProximoTreino = DateTime.UtcNow.AddDays(2)
            };

            await _treinoRepository.SalvarTreino(treino);
            return treino;
        }
    }
}
