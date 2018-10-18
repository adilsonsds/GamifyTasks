using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Domain.Services
{
    public class BaseService<TEntity> : IService<TEntity> where TEntity : class
    {

        private readonly IRepository<TEntity> _repository;

        public BaseService(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public void Adicionar(TEntity obj)
        {
            _repository.Add(obj);
        }

        public void Atualizar(TEntity obj)
        {
            _repository.Update(obj);
        }

        public TEntity ObterPorId(int id)
        {
            return _repository.GetById(id);
        }
    }
}