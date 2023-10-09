using Microsoft.AspNetCore.Mvc;
using TP01_Révisions.Models.EntityFramework;
using TP01_Révisions.Models.Repository;

namespace TP01_Révisions.Controllers
{
    public class TypesProduitController : ControllerBase
    {
        private readonly IDataRepository<TypeProduit> dataRepository;

        public TypesProduitController(IDataRepository<TypeProduit> _dataRepository)
        {
            dataRepository = _dataRepository;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Marque>> GetAllTypesProduit()
        {
            var typesProduit = await dataRepository.GetAllAsync();

            if (typesProduit == null)
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
            var typeProduit = await dataRepository.GetByIdAsync(id);

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
            var typeProduit = await dataRepository.GetByStringAsync(str);

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
            var typeProduitToUpdate = await dataRepository.GetByIdAsync(id);

            // Check if typeProduit exists
            if (typeProduitToUpdate.Value == null)
            {
                return NotFound();
            }

            // Checks if both ids are the same
            if (id != typeProduit.IdTypeProduit)
            {
                return BadRequest();
            }

            await dataRepository.UpdateAsync(typeProduitToUpdate.Value, typeProduit);
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Marque>> PostTypeProduit(TypeProduit typeProduit)
        {
            var existingTypeProduit = await dataRepository.GetByIdAsync(typeProduit.IdTypeProduit);

            // Checks if marque already exists
            if (existingTypeProduit.Value != null)
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
            var typeProduit = await dataRepository.GetByIdAsync(id);

            // Checks if user exists
            if (typeProduit.Value == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(typeProduit.Value);
            return Ok();
        }
    }
}
