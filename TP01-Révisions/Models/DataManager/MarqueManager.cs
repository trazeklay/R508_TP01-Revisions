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
            var marquesDto = await tp01DbContext.Marques.Select(mrqToDto => new MarqueDto
            {
                Id = mrqToDto.IdMarque,
                Nom = mrqToDto.NomMarque,
                NbProduits = mrqToDto.ProduitsMarque.Count()
            }).ToListAsync();

            return marquesDto;
        }

        public async Task<ActionResult<Marque>> GetByIdAsync(int id)
        {
            var marqueDto = await tp01DbContext.Marques.Select(mrqToDto => new Marque
            {
                IdMarque = mrqToDto.IdMarque,
                NomMarque = mrqToDto.NomMarque,
                ProduitsMarque = mrqToDto.ProduitsMarque
            }).FirstOrDefaultAsync(mrqDto => mrqDto.IdMarque == id);

            return marqueDto;
        }


        public async Task<ActionResult<Marque>> GetByStringAsync(string str)
        {
            var marqueDto = await tp01DbContext.Marques.Select(mrqToDto => new Marque
            {
                IdMarque = mrqToDto.IdMarque,
                NomMarque = mrqToDto.NomMarque,
                ProduitsMarque = mrqToDto.ProduitsMarque
            }).FirstOrDefaultAsync(mrqDto => mrqDto.NomMarque == str);

            return marqueDto;
        }

        public async Task<ActionResult<MarqueDto>> GetSummaryByIdAsync(int id)
        {
            var marqueDto = await tp01DbContext.Marques.Select(mrqToDto => new MarqueDto
            {
                Id = mrqToDto.IdMarque,
                Nom = mrqToDto.NomMarque,
                NbProduits = mrqToDto.ProduitsMarque.Count()
            }).FirstOrDefaultAsync(mrqDto => mrqDto.Id == id);

            return marqueDto;
        }

        public async Task<ActionResult<MarqueDto>> GetSummaryByStringAsync(string str)
        {
            var marqueDto = await tp01DbContext.Marques.Select(mrqToDto => new MarqueDto
            {
                Id = mrqToDto.IdMarque,
                Nom = mrqToDto.NomMarque,
                NbProduits = mrqToDto.ProduitsMarque.Count()
            }).FirstOrDefaultAsync(mrqDto => mrqDto.Nom == str);

            return marqueDto;
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
            entityToUpdate.ProduitsMarque = entity.ProduitsMarque;
            await tp01DbContext.SaveChangesAsync();
        }
    }
}
