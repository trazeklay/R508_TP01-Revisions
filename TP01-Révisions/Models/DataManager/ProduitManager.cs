using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP01_Révisions.Models.EntityFramework;
using TP01_Révisions.Models.Repository;

namespace TP01_Révisions.Models.DataManager
{
    public class ProduitManager : IDataRepository<Produit>
    {
        readonly TP01DbContext tP01DbContext;

        public ProduitManager()
        {

        }

        public ProduitManager(TP01DbContext context)
        {
            tP01DbContext = context;
        }

        public async Task<ActionResult<IEnumerable<Produit>>> GetAllAsync()
        {
            return await tP01DbContext.Produits.ToListAsync();
        }

        public async Task<ActionResult<Produit>> GetByIdAsync(int id)
        {
            return await tP01DbContext.Produits.FirstOrDefaultAsync(tpd => tpd.IdProduit == id);
        }

        public async Task<ActionResult<Produit>> GetByStringAsync(string str)
        {
            return await tP01DbContext.Produits.FirstOrDefaultAsync(tpd => tpd.NomProduit == str);
        }

        public async Task AddAsync(Produit entity)
        {
            await tP01DbContext.Produits.AddAsync(entity);
            await tP01DbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Produit entity)
        {
            tP01DbContext.Produits.Remove(entity);
            await tP01DbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Produit entityToUpdate, Produit entity)
        {
            tP01DbContext.Entry(entityToUpdate).State = EntityState.Modified;
            entityToUpdate.IdProduit = entity.IdProduit;
            entityToUpdate.NomProduit = entity.NomProduit;
            entityToUpdate.Description = entity.Description;
            entityToUpdate.NomPhoto = entity.NomPhoto;
            entityToUpdate.UriPhoto = entity.UriPhoto;
            entityToUpdate.IdType = entity.IdType;
            entityToUpdate.IdMarque = entity.IdMarque;
            entityToUpdate.StockReel = entity.StockReel;
            entityToUpdate.StockMin = entity.StockMin;
            entityToUpdate.StockMax = entity.StockMax;
            await tP01DbContext.SaveChangesAsync();
        }
    }
}
