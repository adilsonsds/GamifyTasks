using System;
using System.Collections.Generic;
using System.Linq;
using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Domain.Services
{
    public class GrupoService : BaseService<Grupo>, IGrupoService
    {
        private readonly IGrupoRepository _grupoRepository;
        
        public GrupoService(IGrupoRepository grupoRepository)
            : base(grupoRepository)
        {
            _grupoRepository = grupoRepository;
        }

        public int Adicionar(GrupoDTO grupoDTO)
        {
            if (grupoDTO == null || grupoDTO.Id.HasValue)
                throw new Exception("Solicitação inválida.");

            Grupo grupo = new Grupo();
            grupoDTO.PreencherEntidade(grupo);
            Adicionar(grupo);

            return grupo.Id;
        }

        public void Atualizar(GrupoDTO grupoDTO)
        {
            if (grupoDTO == null || !grupoDTO.Id.HasValue)
                throw new Exception("Solicitação inválida.");

            
            Grupo grupo = ObterPorId(grupoDTO.Id.Value);

            if (grupo == null)
                throw new Exception("Grupo não encontrado.");

            grupoDTO.PreencherEntidade(grupo);
            
            Atualizar(grupo);
        }

        public IEnumerable<GrupoDTO> Listar(int idCaseDeNegocio,int? idGrupo = null)
        {
            List<GrupoDTO> response = new List<GrupoDTO>();

            var grupos = _grupoRepository.Listar();

            foreach (var grupo in grupos)
            {
                GrupoDTO grupoDTO = new GrupoDTO(grupo);
                response.Add(grupoDTO);
            }

            return response;
        }

        public GrupoDTO ObterPorId(int idCase, int idGrupo)
        {
            var grupo = _grupoRepository.Listar().Where(g => g.Id == idGrupo && g.IdCase == idCase).FirstOrDefault();
            GrupoDTO response = new GrupoDTO(grupo);
            return response;
        }


        #region Métodos privados

        #endregion
    }
}