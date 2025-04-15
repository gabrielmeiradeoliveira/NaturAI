public class ResultadoDTO
{
    public bool Sucesso { get; set; }
    public string Mensagem { get; set; }
    public string? Token { get; set; }
    public ResultadoDTO(bool sucesso, string mensagem, string? token = null)
    {
        Sucesso = sucesso;
        Mensagem = mensagem;
        Token = token;
    }
}
