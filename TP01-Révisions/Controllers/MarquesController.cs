using Microsoft.AspNetCore.Mvc;
using TP01_Révisions.Models.EntityFramework;
using TP01_Révisions.Models.Repository;
using AutoMapper;
using TP01_Révisions.Models.DTO;

namespace TP01_Révisions.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarquesController : ControllerBase
    {
        private readonly IDataRepository<Marque> dataRepository;
        private readonly IDataRepositoryDetailDTO<Marque> dataRepositoryMarqueDetailDTO;
        private readonly IDataRepositoryDTO<MarqueDto> dataRepositoryMarqueDTO;
        private readonly IMapper mapper;

        public MarquesController(IDataRepository<Marque> _dataRepository)
        {
            dataRepository = _dataRepository;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MarqueDto, Marque>();
            });

            mapper = config.CreateMapper();
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MarqueDto>> GetAllMarques()
        {
            var marques = await dataRepositoryMarqueDTO.GetAllAsync();

            if (marques == null)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpGet]
        [Route("ById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Marque>> GetMarqueById(int id)
        {
            var marque = await dataRepositoryMarqueDetailDTO.GetByIdAsync(id);

            // Check if marque exists
            if (marque.Value == null)
            {
                return NotFound();
            }

            return marque;
        }

        [HttpGet]
        [Route("ByNom/{str}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Marque>> GetMarqueByNom(string str)
        {
            var marque = await dataRepositoryMarqueDetailDTO.GetByStringAsync(str);

            // Checks if such a marque exists
            if (marque.Value == null)
            {
                return NotFound();
            }

            return marque;
        }

        [HttpGet]
        [Route("SummaryById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MarqueDto>> GetMarqueSummaryById(int id)
        {
            var marque = await dataRepositoryMarqueDTO.GetSummaryByIdAsync(id);

            // Check if marque exists
            if (marque.Value == null)
            {
                return NotFound();
            }

            return marque;
        }

        [HttpGet]
        [Route("SummaryByNom/{str}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MarqueDto>> GetMarqueSummaryByNom(string str)
        {
            var marque = await dataRepositoryMarqueDTO.GetSummaryByStringAsync(str);

            // Checks if such a marque exists
            if (marque.Value == null)
            {
                return NotFound();
            }

            return marque;
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Marque>> PutMarque(int id, Marque marque)
        {
            var marqueDtoToUpdate = await dataRepositoryMarqueDTO.GetSummaryByIdAsync(id);

            // Check if marque exists
            if (marqueDtoToUpdate.Value == null)
            {
                return NotFound();
            }

            // Checks if both ids are the same
            if (id != marque.IdMarque)
            {
                return BadRequest();
            }

            Marque marqueToUpdate = mapper.Map<Marque>(marqueDtoToUpdate.Value);

            await dataRepository.UpdateAsync(marqueToUpdate, marque);
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Marque>> PostMarque(Marque marque)
        {
            var existingMarque = await dataRepositoryMarqueDTO.GetSummaryByIdAsync(marque.IdMarque);

            // Checks if marque already exists
            if (existingMarque.Value != null)
            {
                return BadRequest();
            }

            // Checks if every required data is here
            if (marque.IdMarque == null)
            {
                return BadRequest();
            }

            await dataRepository.AddAsync(marque);
            return Created("localhost", marque);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Marque>> DeleteMarque(int id)
        {
            var marqueDto = await dataRepositoryMarqueDTO.GetSummaryByIdAsync(id);

            // Checks if product exists
            if (marqueDto.Value == null)
            {
                return NotFound();
            }

            Marque marqueToDelete = mapper.Map<Marque>(marqueDto.Value);

            await dataRepository.DeleteAsync(marqueToDelete);
            return Ok();
        }
    }
}