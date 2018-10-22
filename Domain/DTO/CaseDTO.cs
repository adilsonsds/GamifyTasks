using Domain.Entities;

namespace Domain.DTO
{
    public class CaseDTO
    {
        public CaseDTO()
        {

        }

        public CaseDTO(CaseDeNegocio caseDeNegocio)
        {
            Id = caseDeNegocio.Id;
            Nome = caseDeNegocio.Nome;
            TextoDeApresentacao = caseDeNegocio.TextoDeApresentacao;
            PermiteMontarGrupos = caseDeNegocio.PermiteMontarGrupos;
            MinimoAlunosGrupo = caseDeNegocio.MinimoDeAlunosPorGrupo;
            MaximoAlunosGrupo = caseDeNegocio.MaximoDeAlunosPorGrupo;
            IdProfessor = caseDeNegocio.IdProfessor;
            NomeProfessor = caseDeNegocio.Professor.NomeCompleto;
        }

        public int? Id { get; set; }
        public string Nome { get; set; }
        public string TextoDeApresentacao { get; set; }
        public bool PermiteMontarGrupos { get; set; }
        public int? MinimoAlunosGrupo { get; set; }
        public int? MaximoAlunosGrupo { get; set; }
        public int IdProfessor { get; set; }
        public string NomeProfessor { get; set; }
        public bool PermiteSeInscrever { get; set; }

        public void PreencherEntidade(CaseDeNegocio caseDeNegocio)
        {
            caseDeNegocio.Nome = Nome;
            caseDeNegocio.TextoDeApresentacao = TextoDeApresentacao;
            caseDeNegocio.PermiteMontarGrupos = PermiteMontarGrupos;
            caseDeNegocio.MinimoDeAlunosPorGrupo = MinimoAlunosGrupo;
            caseDeNegocio.MaximoDeAlunosPorGrupo = MaximoAlunosGrupo;
        }
    }
}