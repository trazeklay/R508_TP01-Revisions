using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP01_Révisions.Models.EntityFramework;
using TP01_Révisions.Models.Repository;
using TP01_Révisions.Models.DTO;

namespace TP01_Révisions.Models.DataManager
{
    public class ProduitManager : IDataRepository<Produit>, IDataRepositoryProduitDTO, IDataRepositoryProduitDetailDTO
    {
        readonly TP01DbContext tp01DbContext;

        public ProduitManager()
        {

        }

        public ProduitManager(TP01DbContext context)
        {
            tp01DbContext = context;
        }

        public async Task<ActionResult<IEnumerable<ProduitDto>>> GetAllAsync()
        {
            var produitsDto = await tp01DbContext.Produits.Select(pdtToDto => new ProduitDto
            {
                Id = pdtToDto.IdProduit,
                Nom = pdtToDto.NomProduit,
                Marque = pdtToDto.MarquesProduits.NomMarque,
                Type = pdtToDto.TypesProduitProduit.NomTypeProduit
            }).ToListAsync();

            return produitsDto;
        }

        public async Task<ActionResult<ProduitDetailDto>> GetByIdAsync(int id)
        {
            var produitDto = await tp01DbContext.Produits.Select(pdtToDto => new ProduitDetailDto
            {
                Id = pdtToDto.IdProduit,
                Nom = pdtToDto.NomProduit,
                Marque = pdtToDto.MarquesProduits.NomMarque,
                Type = pdtToDto.TypesProduitProduit.NomTypeProduit,
                Description = pdtToDto.Description,
                Nomphoto = pdtToDto.NomPhoto, 
                Uriphoto = pdtToDto.UriPhoto,
                Stock = pdtToDto.StockReel,
                EnReappro = pdtToDto.StockReel < pdtToDto.StockMin
                }).FirstOrDefaultAsync(pdtDto => pdtDto.Id == id);

            return produitDto;
        }
        

        public async Task<ActionResult<ProduitDetailDto>> GetByStringAsync(string str)
        {
            var produitDto = await tp01DbContext.Produits.Select(pdtToDto => new ProduitDetailDto
            {
                Id = pdtToDto.IdProduit,
                Nom = pdtToDto.NomProduit,
                Marque = pdtToDto.MarquesProduits.NomMarque,
                Type = pdtToDto.TypesProduitProduit.NomTypeProduit,
                Description = pdtToDto.Description,
                Nomphoto = pdtToDto.NomPhoto,
                Uriphoto = pdtToDto.UriPhoto,
                Stock = pdtToDto.StockReel,
                EnReappro = pdtToDto.StockReel < pdtToDto.StockMin
            }).FirstOrDefaultAsync(pdtDto => pdtDto.Nom == str);

            return produitDto;
        }
        
        public async Task<ActionResult<ProduitDto>> GetSummaryByIdAsync(int id)
        {
            var produitDto = await tp01DbContext.Produits.Select(pdtToDto => new ProduitDto
            {
                Id = pdtToDto.IdProduit,
                Nom = pdtToDto.NomProduit,
                Marque = pdtToDto.MarquesProduits.NomMarque,
                Type = pdtToDto.TypesProduitProduit.NomTypeProduit
            }).FirstOrDefaultAsync(pdtDto => pdtDto.Id == id);

            return produitDto;
        }

        public async Task<ActionResult<ProduitDto>> GetSummaryByStringAsync(string str)
        {
            var produitDto = await tp01DbContext.Produits.Select(pdtToDto => new ProduitDto
            {
                Id = pdtToDto.IdProduit,
                Nom = pdtToDto.NomProduit,
                Marque = pdtToDto.MarquesProduits.NomMarque,
                Type = pdtToDto.TypesProduitProduit.NomTypeProduit
            }).FirstOrDefaultAsync(pdtDto => pdtDto.Nom == str);

            return produitDto;
        }

        public async Task AddAsync(Produit entity)
        {
            await tp01DbContext.Produits.AddAsync(entity);
            await tp01DbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Produit entity)
        {
            tp01DbContext.Produits.Remove(entity);
            await tp01DbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Produit entityToUpdate, Produit entity)
        {
            tp01DbContext.Entry(entityToUpdate).State = EntityState.Modified;
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
            await tp01DbContext.SaveChangesAsync();
        }
    }
}
