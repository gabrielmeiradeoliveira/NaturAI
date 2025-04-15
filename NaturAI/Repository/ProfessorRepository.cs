using Dapper;
using Npgsql;

namespace NaturAI.Repository
{
    public class ProfessorRepository : IProfessorRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public ProfessorRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _connectionString = _configuration.GetConnectionString("DefaultConnection")
                                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' não foi configurada.");
        }

        public async Task<bool> VincularAlunoAoProfessor(string emailAluno, string emailProfessor)
        {
            using var connection = new NpgsqlConnection(_connectionString);

            const string verificaVinculoSql = @"
                SELECT COUNT(1) 
                FROM usuario.professor_aluno 
                WHERE id_professor = (SELECT id FROM usuario.usuario WHERE email = @EmailProfessor)
                  AND id_aluno = (SELECT id FROM usuario.usuario WHERE email = @EmailAluno);
            ";

            var vinculoExistente = await connection.ExecuteScalarAsync<int>(verificaVinculoSql, new
            {
                EmailProfessor = emailProfessor,
                EmailAluno = emailAluno
            });

            if (vinculoExistente > 0)
            {
                return false;
            }

            const string verificaProfessorComoAlunoSql = @"
                SELECT COUNT(1)
                FROM usuario.usuario
                WHERE email = @EmailProfessor AND tipo = 'Professor';
            ";

            var professorComoAluno = await connection.ExecuteScalarAsync<int>(verificaProfessorComoAlunoSql, new
            {
                EmailProfessor = emailProfessor
            });

            if (professorComoAluno > 0)
            {
                return false;
            }

            const string sql = @"
                INSERT INTO usuario.professor_aluno (id_professor, id_aluno)
                SELECT 
                    professor.id,
                    aluno.id
                FROM usuario.usuario AS professor,
                     usuario.usuario AS aluno
                WHERE professor.email = @EmailProfessor
                  AND aluno.email = @EmailAluno
                ON CONFLICT (id_professor, id_aluno) DO NOTHING;
            ";

            var result = await connection.ExecuteAsync(sql, new
            {
                EmailProfessor = emailProfessor,
                EmailAluno = emailAluno
            });

            return result > 0;
        }
    }
}
