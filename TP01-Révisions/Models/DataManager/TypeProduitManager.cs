using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP01_Révisions.Models.EntityFramework;
using TP01_Révisions.Models.Repository;

namespace TP01_Révisions.Models.DataManager
{
    public class TypeProduitManager : IDataRepository<TypeProduit>
    {
        readonly TP01DbContext tP01DbContext;

        public TypeProduitManager()
        {
            
        }

        public TypeProduitManager(TP01DbContext context)
        {
            tP01DbContext = context;
        }

        public async Task<ActionResult<IEnumerable<TypeProduit>>> GetAllAsync()
        {
            return await tP01DbContext.TypesProduit.ToListAsync();
        }

        public async Task<ActionResult<TypeProduit>> GetByIdAsync(int id)
        {
            return await tP01DbContext.TypesProduit.FirstOrDefaultAsync(tpd => tpd.IdTypeProduit == id);
        }

        public async Task<ActionResult<TypeProduit>> GetByStringAsync(string str)
        {
            return await tP01DbContext.TypesProduit.FirstOrDefaultAsync(tpd => tpd.NomTypeProduit == str);
        }

        public async Task AddAsync(TypeProduit entity)
        {
            await tP01DbContext.TypesProduit.AddAsync(entity);
            await tP01DbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TypeProduit entity)
        {
            tP01DbContext.TypesProduit.Remove(entity);
            await tP01DbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(TypeProduit entityToUpdate, TypeProduit entity)
        {
            tP01DbContext.Entry(entityToUpdate).State = EntityState.Modified;
            entityToUpdate.IdTypeProduit = entity.IdTypeProduit;
            entityToUpdate.NomTypeProduit = entity.NomTypeProduit;
            await tP01DbContext.SaveChangesAsync();
        }
    }
}
