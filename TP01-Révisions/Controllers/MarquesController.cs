using Microsoft.AspNetCore.Mvc;
using TP01_Révisions.Models.EntityFramework;
using TP01_Révisions.Models.Repository;

namespace TP01_Révisions.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarquesController : ControllerBase
    {
        private readonly IDataRepository<Marque> dataRepository;

        public MarquesController(IDataRepository<Marque> _dataRepository)
        {
            dataRepository = _dataRepository;
        }

        [HttpGet]
        [Route("ById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Marque>> GetMarqueById(int id)
        {
            var marque = await dataRepository.GetByIdAsync(id);

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
            var marque = await dataRepository.GetByStringAsync(str);

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
            var marqueToUpdate = await dataRepository.GetByIdAsync(id);

            // Check if marque exists
            if (marqueToUpdate.Value == null)
            {
                return NotFound();
            }

            // Checks if both ids are the same
            if (marqueToUpdate.Value.IdMarque != marque.IdMarque)
            {
                return BadRequest();
            }

            await dataRepository.UpdateAsync(marqueToUpdate.Value, marque);
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Marque>> PostMarque(Marque marque)
        {
            var existingMarque = await dataRepository.GetByIdAsync(marque.IdMarque);

            // Checks if marque already exists
            if (existingMarque.Value != null)
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
            var marque = await dataRepository.GetByIdAsync(id);

            // Checks if user exists
            if (marque.Value == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(marque.Value);
            return Ok();
        }
    }
}
