using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP01_Révisions.Models.EntityFramework;
using TP01_Révisions.Models.Repository;
using TP01_Révisions.Models.DTO;

namespace TP01_Révisions.Models.DataManager
{
    public class MarqueManager : IDataRepository<Marque>, IDataRepositoryMarqueDTO
    {
        readonly TP01DbContext tp01DbContext;

        public MarqueManager()
        {

        }

        public MarqueManager(TP01DbContext context)
        {
            tp01DbContext = context;
        }

        public async Task<ActionResult<IEnumerable<MarqueDto>>> GetAllAsync()
        {
            var marquesDto = await tp01DbContext.Produits.Select(mrqtoDto => new MarqueDto
            {
                Id = mrqtoDto.IdProduit,
                Nom = mrqtoDto.NomProduit,
                NbProduits = mrqtoDto.MarquesProduits.ProduitsMarque.Count()
            }).ToListAsync();

            return marquesDto;
        }

        public async Task<ActionResult<Marque>> GetByIdAsync(int id)
        {
            return await tp01DbContext.Marques.FirstOrDefaultAsync(mar => mar.IdMarque == id);
        }

        public async Task<ActionResult<Marque>> GetByStringAsync(string str)
        {
            return await tp01DbContext.Marques.FirstOrDefaultAsync(mar => mar.NomMarque == str);
        }

        public async Task AddAsync(Marque entity)
        {
            await tp01DbContext.Marques.AddAsync(entity);
            await tp01DbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Marque entity)
        {
            tp01DbContext.Marques.Remove(entity);
            await tp01DbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Marque entityToUpdate, Marque entity)
        {
            tp01DbContext.Entry(entityToUpdate).State = EntityState.Modified;
            entityToUpdate.IdMarque = entity.IdMarque;
            entityToUpdate.NomMarque = entity.NomMarque;
            await tp01DbContext.SaveChangesAsync();
        }

        public Task<ActionResult<MarqueDto>> GetSummaryByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<MarqueDto>> GetSummaryByStringAsync(string str)
        {
            throw new NotImplementedException();
        }

        Task<ActionResult<IEnumerable<MarqueDto>>> IDataRepositoryDTO<MarqueDto>.GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
