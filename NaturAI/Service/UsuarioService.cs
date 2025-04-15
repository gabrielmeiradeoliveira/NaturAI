using NaturAI.Model;
using NaturAI.Repository;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly JwtService _jwtService;

    public UsuarioService(IUsuarioRepository usuarioRepository, JwtService jwtService)
    {
        _usuarioRepository = usuarioRepository;
        _jwtService = jwtService;
    }

    public async Task<ResultadoDTO> Registrar(UsuarioDTO novoUsuario)
    {
        if (await _usuarioRepository.ExistePorEmail(novoUsuario.Email))
            return new ResultadoDTO(false, "Já existe um usuário com esse e-mail.");

        if (novoUsuario.Senha != novoUsuario.ConfirmarSenha)
            return new ResultadoDTO(false, "As senhas não coincidem.");

        var resultado = await _usuarioRepository.Adicionar(novoUsuario);
        return resultado ?
            new ResultadoDTO(true, "Usuário registrado com sucesso.") :
            new ResultadoDTO(false, "Erro ao registrar o usuário.");
    }

    public async Task<ResultadoDTO> Login(LoginDTO login)
    {
        var usuario = await _usuarioRepository.ObterPorCredenciais(login.Email, login.Senha);

        if (usuario == null)
            return new ResultadoDTO(false, "Email ou senha incorretos.");

        var token = _jwtService.GerarToken(usuario);

        return new ResultadoDTO(true, "Login realizado com sucesso.", token);
    }

}
