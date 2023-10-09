using Microsoft.AspNetCore.Mvc;
using TP01_Révisions.Models.DTO;

namespace TP01_Révisions.Models.Repository
{
    public interface IDataRepositoryDTO<TEntity>
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetAllAsync();
        Task<ActionResult<TEntity>> GetSummaryByIdAsync(int id);
        Task<ActionResult<TEntity>> GetSummaryByStringAsync(string str);
    }
}
