namespace NaturAI.Model
{
    public class TreinoGeradoDTO
    {
        public int UsuarioId { get; set; }
        public string Descricao { get; set; }
        public DateTime DataGeracao { get; set; }
        public DateTime ProximoTreino { get; set; }
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
    }

}
