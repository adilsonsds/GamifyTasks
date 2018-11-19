namespace Domain.DTO
{
    public class AlunoDoCaseDTO
    {
        public int IdUsuario { get; set; }
        public string NomeCompleto { get; set; }
        public int? IdGrupo { get; set; }
        public string NomeGrupo { get; set; }
        public int Pontos { get; set; }
    }
}