using NaturAI.Model;

public interface IUsuarioService
{
    Task<ResultadoDTO> Login(LoginDTO login);
    Task<ResultadoDTO> Registrar(UsuarioDTO novoUsuario);
}