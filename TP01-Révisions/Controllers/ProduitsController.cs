using Microsoft.AspNetCore.Mvc;
using TP01_Révisions.Models.EntityFramework;
using TP01_Révisions.Models.Repository;

namespace TP01_Révisions.Controllers
{
    public class ProduitsController : ControllerBase
    {
        private readonly IDataRepository<Produit> dataRepository;

        public ProduitsController(IDataRepository<Produit> _dataRepository)
        {
            dataRepository = _dataRepository;
        }

        [HttpGet]
        [Route("ById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Produit>> GetProduitById(int id)
        {
            var produit = await dataRepository.GetByIdAsync(id);

            // Check if produit exists
            if (produit.Value == null)
            {
                return NotFound();
            }

            return produit;
        }

        [HttpGet]
        [Route("ByNom/{str}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Produit>> GetProduitByNom(string str)
        {
            var produit = await dataRepository.GetByStringAsync(str);

            // Checks if such a produit exists
            if (produit.Value == null)
            {
                return NotFound();
            }

            return produit;
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Produit>> PutProduit(int id, Produit produit)
        {
            var produitToUpdate = await dataRepository.GetByIdAsync(id);

            // Check if produit exists
            if (produitToUpdate.Value == null)
            {
                return NotFound();
            }

            // Checks if both ids are the same
            if (id != produit.IdProduit)
            {
                return BadRequest();
            }

            await dataRepository.UpdateAsync(produitToUpdate.Value, produit);
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Produit>> PostProduit(Produit produit)
        {
            var existingProduit = await dataRepository.GetByIdAsync(produit.IdProduit);

            // Checks if marque already exists
            if (existingProduit.Value != null)
            {
                return BadRequest();
            }

            // Checks if every required data is here
            if (produit.IdMarque == null || produit.IdType == null)
            {
                return BadRequest();
            }

            await dataRepository.AddAsync(produit);
            return Created("localhost", produit);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Produit>> DeleteProduit(int id)
        {
            var produit = await dataRepository.GetByIdAsync(id);

            // Checks if product exists
            if (produit.Value == null)
            {
                return NotFound();
            }

            await dataRepository.DeleteAsync(produit.Value);
            return Ok();
        }
    }
}
