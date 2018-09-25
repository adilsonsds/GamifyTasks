using Domain.Entities;

namespace Api.Models.Case
{
    public class CaseResponse
    {

        public CaseResponse(CaseDeNegocio caseDeNegocio)
        {
            
        }

        public int Id { get; set; }
        public string Nome { get; set; }
    }
}