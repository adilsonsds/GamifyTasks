namespace Api.Models
{
    public class CaseModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string TextoDeApresentacao { get; set; }
        public bool PermiteMontarGrupos { get; set; }
        public int MinimoDeAlunosPorGrupo { get; set; }
        public int MaximoDeAlunosPorGrupo { get; set; }
    }
}