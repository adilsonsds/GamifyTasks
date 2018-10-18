using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface IService<TEntity> where TEntity : class
    {
        TEntity ObterPorId(int id);

        void Adicionar(TEntity obj);

        void Atualizar(TEntity obj);

    }
}