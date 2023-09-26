using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP01_Révisions.Models.EntityFramework;
using TP01_Révisions.Models.Repository;

namespace TP01_Révisions.Models.DataManager
{
    public class MarqueManager : IDataRepository<Marque>
    {
        readonly TP01DbContext tP01DbContext;

        public MarqueManager()
        {

        }

        public MarqueManager(TP01DbContext context)
        {
            tP01DbContext = context;
        }

        public async Task<ActionResult<IEnumerable<Marque>>> GetAllAsync()
        {
            return await tP01DbContext.Marques.ToListAsync();
        }

        public async Task<ActionResult<Marque>> GetByIdAsync(int id)
        {
            return await tP01DbContext.Marques.FirstOrDefaultAsync(mar => mar.IdMarque == id);
        }

        public async Task<ActionResult<Marque>> GetByStringAsync(string str)
        {
            return await tP01DbContext.Marques.FirstOrDefaultAsync(mar => mar.NomMarque == str);
        }

        public async Task AddAsync(Marque entity)
        {
            await tP01DbContext.Marques.AddAsync(entity);
            await tP01DbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Marque entity)
        {
            tP01DbContext.Marques.Remove(entity);
            await tP01DbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Marque entityToUpdate, Marque entity)
        {
            tP01DbContext.Entry(entityToUpdate).State = EntityState.Modified;
            entityToUpdate.IdMarque = entity.IdMarque;
            entityToUpdate.NomMarque = entity.NomMarque;
            await tP01DbContext.SaveChangesAsync();
        }
    }
}
