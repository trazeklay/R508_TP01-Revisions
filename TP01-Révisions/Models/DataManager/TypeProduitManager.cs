using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP01_Révisions.Models.DTO;
using TP01_Révisions.Models.EntityFramework;
using TP01_Révisions.Models.Repository;

namespace TP01_Révisions.Models.DataManager
{
    public class TypeProduitManager : IDataRepository<TypeProduit>, IDataRepositoryDTO<TypeProduitDto>, IDataRepositoryDetailDTO<TypeProduit>
    {
        readonly TP01DbContext tp01DbContext;

        public TypeProduitManager()
        {
            
        }

        public TypeProduitManager(TP01DbContext context)
        {
            tp01DbContext = context;
        }

        public async Task<ActionResult<IEnumerable<TypeProduitDto>>> GetAllAsync()
        {
            var marquesDto = await tp01DbContext.TypesProduit.Select(mrqToDto => new TypeProduitDto
            {
                Id = mrqToDto.IdTypeProduit,
                Nom = mrqToDto.NomTypeProduit,
                NbTypeProduits = mrqToDto.ProduitsTypesProduit.Count()
            }).ToListAsync();

            return marquesDto;
        }

        public async Task<ActionResult<TypeProduit>> GetByIdAsync(int id)
        {
            var marqueDto = await tp01DbContext.TypesProduit.Select(mrqToDto => new TypeProduit
            {
                IdTypeProduit = mrqToDto.IdTypeProduit,
                NomTypeProduit = mrqToDto.NomTypeProduit,
                ProduitsTypesProduit = mrqToDto.ProduitsTypesProduit
            }).FirstOrDefaultAsync(mrqDto => mrqDto.IdTypeProduit == id);

            return marqueDto;
        }


        public async Task<ActionResult<TypeProduit>> GetByStringAsync(string str)
        {
            var marqueDto = await tp01DbContext.TypesProduit.Select(mrqToDto => new TypeProduit
            {
                IdTypeProduit = mrqToDto.IdTypeProduit,
                NomTypeProduit = mrqToDto.NomTypeProduit,
                ProduitsTypesProduit = mrqToDto.ProduitsTypesProduit
            }).FirstOrDefaultAsync(mrqDto => mrqDto.NomTypeProduit == str);

            return marqueDto;
        }

        public async Task<ActionResult<TypeProduitDto>> GetSummaryByIdAsync(int id)
        {
            var marqueDto = await tp01DbContext.TypesProduit.Select(mrqToDto => new TypeProduitDto
            {
                Id = mrqToDto.IdTypeProduit,
                Nom = mrqToDto.NomTypeProduit,
                NbTypeProduits = mrqToDto.ProduitsTypesProduit.Count()
            }).FirstOrDefaultAsync(mrqDto => mrqDto.Id == id);

            return marqueDto;
        }

        public async Task<ActionResult<TypeProduitDto>> GetSummaryByStringAsync(string str)
        {
            var marqueDto = await tp01DbContext.TypesProduit.Select(mrqToDto => new TypeProduitDto
            {
                Id = mrqToDto.IdTypeProduit,
                Nom = mrqToDto.NomTypeProduit,
                NbTypeProduits = mrqToDto.ProduitsTypesProduit.Count()
            }).FirstOrDefaultAsync(mrqDto => mrqDto.Nom == str);

            return marqueDto;
        }

        public async Task AddAsync(TypeProduit entity)
        {
            await tp01DbContext.TypesProduit.AddAsync(entity);
            await tp01DbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TypeProduit entity)
        {
            tp01DbContext.TypesProduit.Remove(entity);
            await tp01DbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(TypeProduit entityToUpdate, TypeProduit entity)
        {
            tp01DbContext.Entry(entityToUpdate).State = EntityState.Modified;
            entityToUpdate.IdTypeProduit = entity.IdTypeProduit;
            entityToUpdate.NomTypeProduit = entity.NomTypeProduit;
            entityToUpdate.ProduitsTypesProduit = entity.ProduitsTypesProduit;
            await tp01DbContext.SaveChangesAsync();
        }
    }
}
