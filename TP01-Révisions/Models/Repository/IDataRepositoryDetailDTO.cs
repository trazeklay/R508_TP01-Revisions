using Microsoft.AspNetCore.Mvc;
using TP01_Révisions.Models.DTO;

namespace TP01_Révisions.Models.Repository
{
    public interface IDataRepositoryDetailDTO<TEntity>
    {
        Task<ActionResult<TEntity>> GetByIdAsync(int id);
        Task<ActionResult<TEntity>> GetByStringAsync(string str);
    }
}
