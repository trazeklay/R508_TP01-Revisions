using Microsoft.AspNetCore.Mvc;

namespace TP01_Révisions.Models.Repository
{
    public interface IDataRepository<TEntity>
    {
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entityToUpdate, TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
