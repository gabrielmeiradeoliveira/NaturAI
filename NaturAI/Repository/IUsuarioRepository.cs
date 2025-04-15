using NaturAI.Model;

namespace NaturAI.Repository
{
    public interface IUsuarioRepository
    {
        Task<bool> Adicionar(UsuarioDTO novoUsuario);
        Task<bool> ExistePorEmail(string email);
        Task<UsuarioDTO> ObterPorCredenciais(string email, string senha);
        Task<UsuarioDTO?> ObterPorEmail(string email);
    }
}