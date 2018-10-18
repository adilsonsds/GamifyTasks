using Domain.Entities;

namespace Api.Models
{
    public class CaseModel
    {
        public CaseModel()
        {

        }

        public CaseModel(CaseDeNegocio caseDeNegocios)
        {
            Id = caseDeNegocios.Id;
            Nome = caseDeNegocios.Nome;
            TextoDeApresentacao = caseDeNegocios.TextoDeApresentacao;
            PermiteMontarGrupos = caseDeNegocios.PermiteMontarGrupos;
            MinimoAlunosGrupo = caseDeNegocios.MinimoDeAlunosPorGrupo;
            MaximoAlunosGrupo = caseDeNegocios.MaximoDeAlunosPorGrupo;
            IdProfessor = caseDeNegocios.IdProfessor;
            NomeProfessor = caseDeNegocios.Professor.NomeCompleto;
        }

        public CaseModel(CaseDeNegocio caseDeNegocios, Domain.Entities.Usuario professor)
        : this(caseDeNegocios)
        {
            // IdProfessor = professor.Id;
            // NomeProfessor = professor.NomeCompleto;
            if (caseDeNegocios.Professor != null)
            {
                IdProfessor = caseDeNegocios.Professor.Id;
                NomeProfessor = caseDeNegocios.Professor.NomeCompleto;
            }
        }

        public int? Id { get; set; }
        public string Nome { get; set; }
        public string TextoDeApresentacao { get; set; }
        public bool PermiteMontarGrupos { get; set; }
        public int? MinimoAlunosGrupo { get; set; }
        public int? MaximoAlunosGrupo { get; set; }
        public int IdProfessor { get; set; }
        public string NomeProfessor { get; set; }

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