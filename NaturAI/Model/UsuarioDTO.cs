using NaturAI.Model.Enum;

namespace NaturAI.Model
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string ConfirmarSenha { get; set; }
        public DateTime DataNascimento { get; set; }
        public SexoEnum Sexo { get; set; }
        public float Peso { get; set; }
        public int DiasTreino { get; set; }
        public IntensidadeTreinoEnum Intensidade { get; set; }
        public NivelTreinoEnum NivelTreino { get; set; }
        public PermissaoEnum Permissao { get; set; }
        public List<TipoTreinoEnum> TipoTreino { get; set; }
        public List<FocoMuscularEnum> FocoMuscular { get; set; }
    }
}
