using Dapper;
using NaturAI.Model;
using Npgsql;

namespace NaturAI.Repository
{
    public class GeradorTreinoRepository : IGeradorTreinoRepository
    {
        private readonly string _connectionString;

        public GeradorTreinoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                                ?? throw new InvalidOperationException("Connection string não configurada.");
        }

        public async Task SalvarTreino(TreinoDTO treino)
        {
            using var connection = new NpgsqlConnection(_connectionString);

            const string sql = @"
                INSERT INTO usuario.treinos (usuario_id, descricao, data_geracao, proximo_treino)
                VALUES (@UsuarioId, @Descricao, @DataGeracao, @ProximoTreino);";

            await connection.ExecuteAsync(sql, treino);
        }
    }
}
