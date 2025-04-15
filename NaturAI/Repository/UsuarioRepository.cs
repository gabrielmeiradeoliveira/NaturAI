using Dapper;
using NaturAI.Model;
using NaturAI.Model.Enum;
using Npgsql;

namespace NaturAI.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public UsuarioRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _connectionString = _configuration.GetConnectionString("DefaultConnection")
                                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' não foi configurada.");
        }

        public async Task<bool> ExistePorEmail(string email)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            const string sql = "SELECT COUNT(1) FROM usuario.usuario WHERE email = @Email";
            var count = await connection.ExecuteScalarAsync<int>(sql, new { Email = email });
            return count > 0;
        }

        public async Task<bool> Adicionar(UsuarioDTO novoUsuario)
        {
            if (novoUsuario.Senha != novoUsuario.ConfirmarSenha)
                throw new ArgumentException("Senha e confirmação não coincidem.");

            var senhaHash = BCrypt.Net.BCrypt.HashPassword(novoUsuario.Senha);

            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            using var transaction = await connection.BeginTransactionAsync();

            try
            {
                const string insertUsuarioSql = @"
                    INSERT INTO usuario.usuario 
                    (nome, email, senha_hash, data_nascimento, sexo, peso, dias_treino, intensidade_treino, nivel_treino, permissao)
                    VALUES (@Nome, @Email, @Senha, @DataNascimento, @Sexo, @Peso, @DiasTreino, @Intensidade, @NivelTreino, @Permissao)
                    RETURNING id";

                var usuarioId = await connection.ExecuteScalarAsync<int>(insertUsuarioSql, new
                {
                    Nome = novoUsuario.Nome,
                    Email = novoUsuario.Email,
                    Senha = senhaHash,
                    DataNascimento = novoUsuario.DataNascimento,
                    Sexo = novoUsuario.Sexo == SexoEnum.Masculino ? "M" : "F",
                    Peso = novoUsuario.Peso,
                    DiasTreino = novoUsuario.DiasTreino,
                    Intensidade = (int)novoUsuario.Intensidade,
                    NivelTreino = (int)novoUsuario.NivelTreino,
                    Permissao = (int)novoUsuario.Permissao
                }, transaction);

                foreach (var foco in novoUsuario.FocoMuscular)
                {
                    await connection.ExecuteAsync(
                        "INSERT INTO usuario.usuario_foco_muscular (id_usuario, id_foco_muscular) VALUES (@UsuarioId, @FocoId)",
                        new { UsuarioId = usuarioId, FocoId = (int)foco + 1 }, transaction);
                }

                foreach (var tipo in novoUsuario.TipoTreino)
                {
                    await connection.ExecuteAsync(
                        "INSERT INTO usuario.usuario_tipo_treino (id_usuario, id_tipo_treino) VALUES (@UsuarioId, @TipoId)",
                        new { UsuarioId = usuarioId, TipoId = (int)tipo }, transaction);
                }

                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<UsuarioDTO?> ObterPorCredenciais(string email, string senha)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            const string sql = @"
                SELECT  
                    id                  AS ""Id"",
                    nome                AS ""Nome"",
                    email               AS ""Email"",
                    senha_hash          AS ""Senha"",
                    data_nascimento     AS ""DataNascimento"",
                    sexo                AS ""Sexo"",
                    peso                AS ""Peso"",
                    dias_treino         AS ""DiasTreino"",
                    intensidade_treino  AS ""IntensidadeTreino"",
                    nivel_treino        AS ""NivelTreino"",
                    permissao           AS ""Permissao""
                FROM usuario.usuario
                WHERE email = @Email";

            var usuario = await connection.QueryFirstOrDefaultAsync<UsuarioDTO>(sql, new { Email = email });

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(senha, usuario.Senha))
                return null;

            return usuario;
        }

        public async Task<UsuarioDTO?> ObterPorEmail(string email)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            const string sql = @"
            SELECT  
                id                  AS ""Id"",
                nome                AS ""Nome"",
                email               AS ""Email"",
                senha_hash          AS ""Senha"",
                data_nascimento     AS ""DataNascimento"",
                sexo                AS ""Sexo"",
                peso                AS ""Peso"",
                dias_treino         AS ""DiasTreino"",
                intensidade_treino  AS ""IntensidadeTreino"",
                nivel_treino        AS ""NivelTreino"",
                permissao           AS ""Permissao""
            FROM usuario.usuario
            WHERE email = @Email";

            var usuario = await connection.QueryFirstOrDefaultAsync<UsuarioDTO>(sql, new { Email = email });
            return usuario;
        }
    }
}
