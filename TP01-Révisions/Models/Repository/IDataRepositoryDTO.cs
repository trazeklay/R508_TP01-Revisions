using Microsoft.AspNetCore.Mvc;

namespace TP01_Révisions.Models.Repository
{
    public interface IDataRepositoryDTO<TEntity>
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetAllAsync();
        Task<ActionResult<TEntity>> GetSummaryByIdAsync(int id);
        Task<ActionResult<TEntity>> GetSummaryByStringAsync(string str);
    }
}
