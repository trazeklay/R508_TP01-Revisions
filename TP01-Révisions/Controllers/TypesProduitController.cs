using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TP01_Révisions.Models.DTO;
using TP01_Révisions.Models.EntityFramework;
using TP01_Révisions.Models.Repository;

namespace TP01_Révisions.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeProduitsController : ControllerBase
    {
        private readonly IDataRepository<TypeProduit> dataRepository;
        private readonly IDataRepositoryDetailDTO<TypeProduit> dataRepositoryTypeProduitDetailDTO;
        private readonly IDataRepositoryTypeProduitDTO dataRepositoryTypeProduitDTO;
        private readonly IMapper mapper;

        public TypeProduitsController(IDataRepository<TypeProduit> _dataRepository)
        {
            dataRepository = _dataRepository;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TypeProduitDto, TypeProduit>();
            });

            mapper = config.CreateMapper();
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TypeProduitDto>> GetAllTypeProduits()
        {
            var typeProduits = await dataRepositoryTypeProduitDTO.GetAllAsync();

            if (typeProduits == null)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpGet]
        [Route("ById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TypeProduit>> GetTypeProduitById(int id)
        {
            var typeProduit = await dataRepositoryTypeProduitDetailDTO.GetByIdAsync(id);

            // Check if typeProduit exists
            if (typeProduit.Value == null)
            {
                return NotFound();
            }

            return typeProduit;
        }

        [HttpGet]
        [Route("ByNom/{str}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TypeProduit>> GetTypeProduitByNom(string str)
        {
            var typeProduit = await dataRepositoryTypeProduitDetailDTO.GetByStringAsync(str);

            // Checks if such a typeProduit exists
            if (typeProduit.Value == null)
            {
                return NotFound();
            }

            return typeProduit;
        }

        [HttpGet]
        [Route("SummaryById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TypeProduitDto>> GetTypeProduitSummaryById(int id)
        {
            var typeProduit = await dataRepositoryTypeProduitDTO.GetSummaryByIdAsync(id);

            // Check if typeProduit exists
            if (typeProduit.Value == null)
            {
                return NotFound();
            }

            return typeProduit;
        }

        [HttpGet]
        [Route("SummaryByNom/{str}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TypeProduitDto>> GetTypeProduitSummaryByNom(string str)
        {
            var typeProduit = await dataRepositoryTypeProduitDTO.GetSummaryByStringAsync(str);

            // Checks if such a typeProduit exists
            if (typeProduit.Value == null)
            {
                return NotFound();
            }

            return typeProduit;
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TypeProduit>> PutTypeProduit(int id, TypeProduit typeProduit)
        {
            var typeProduitDtoToUpdate = await dataRepositoryTypeProduitDTO.GetSummaryByIdAsync(id);

            // Check if typeProduit exists
            if (typeProduitDtoToUpdate.Value == null)
            {
                return NotFound();
            }

            // Checks if both ids are the same
            if (id != typeProduit.IdTypeProduit)
            {
                return BadRequest();
            }

            TypeProduit typeProduitToUpdate = mapper.Map<TypeProduit>(typeProduitDtoToUpdate.Value);

            await dataRepository.UpdateAsync(typeProduitToUpdate, typeProduit);
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TypeProduit>> PostTypeProduit(TypeProduit typeProduit)
        {
            var existingTypeProduit = await dataRepositoryTypeProduitDTO.GetSummaryByIdAsync(typeProduit.IdTypeProduit);

            // Checks if typeProduit already exists
            if (existingTypeProduit.Value != null)
            {
                return BadRequest();
            }

            // Checks if every required data is here
            if (typeProduit.IdTypeProduit == null)
            {
                return BadRequest();
            }

            await dataRepository.AddAsync(typeProduit);
            return Created("localhost", typeProduit);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TypeProduit>> DeleteTypeProduit(int id)
        {
            var typeProduitDto = await dataRepositoryTypeProduitDTO.GetSummaryByIdAsync(id);

            // Checks if product exists
            if (typeProduitDto.Value == null)
            {
                return NotFound();
            }

            TypeProduit typeProduitToDelete = mapper.Map<TypeProduit>(typeProduitDto.Value);

            await dataRepository.DeleteAsync(typeProduitToDelete);
            return Ok();
        }
    }
}
