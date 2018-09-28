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
            MinimoDeAlunosPorGrupo = caseDeNegocios.MinimoDeAlunosPorGrupo;
            MaximoDeAlunosPorGrupo = caseDeNegocios.MaximoDeAlunosPorGrupo;

        }

        public int? Id { get; set; }
        public string Nome { get; set; }
        public string TextoDeApresentacao { get; set; }
        public bool PermiteMontarGrupos { get; set; }
        public int? MinimoDeAlunosPorGrupo { get; set; }
        public int? MaximoDeAlunosPorGrupo { get; set; }


        public void PreencherEntidade(CaseDeNegocio caseDeNegocio)
        {
            caseDeNegocio.Nome = Nome;
            caseDeNegocio.TextoDeApresentacao = TextoDeApresentacao;
            caseDeNegocio.PermiteMontarGrupos = PermiteMontarGrupos;
            caseDeNegocio.MinimoDeAlunosPorGrupo = MinimoDeAlunosPorGrupo;
            caseDeNegocio.MaximoDeAlunosPorGrupo = MaximoDeAlunosPorGrupo;
        }
    }
}