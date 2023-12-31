﻿using Microsoft.AspNetCore.Mvc;
using TP01_Révisions.Models.DTO;
using TP01_Révisions.Models.EntityFramework;
using TP01_Révisions.Models.Repository;
using AutoMapper;

namespace TP01_Révisions.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProduitsController : ControllerBase
    {
        private readonly IDataRepository<Produit> dataRepository;
        private readonly IDataRepositoryDTO<ProduitDto> dataRepositoryProduitDTO;
        private readonly IDataRepositoryDetailDTO<ProduitDetailDto> dataRepositoryProduitDetailDTO;
        private readonly IMapper mapper;

        public ProduitsController(IDataRepository<Produit> _dataRepository, IDataRepositoryDTO<ProduitDto> _dataRepositoryDTO, IDataRepositoryDetailDTO<ProduitDetailDto> _dataRepositoryProduitDetailDTO)
        {
            dataRepository = _dataRepository;
            dataRepositoryProduitDTO = _dataRepositoryDTO;
            dataRepositoryProduitDetailDTO = _dataRepositoryProduitDetailDTO;

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ProduitDto, Produit>();
                cfg.CreateMap<ProduitDetailDto, Produit>();
            });

            mapper = config.CreateMapper();
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProduitDto>> GetAllProduits()
        {
            var produits = await dataRepositoryProduitDTO.GetAllAsync();

            if (produits == null)
            {
                return NotFound();
            }

            return Ok(produits.Value);
        }

        [HttpGet]
        [Route("ById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProduitDetailDto>> GetProduitById(int id)
        {
            var produit = await dataRepositoryProduitDetailDTO.GetByIdAsync(id);

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
        public async Task<ActionResult<ProduitDetailDto>> GetProduitByNom(string str)
        {
            var produit = await dataRepositoryProduitDetailDTO.GetByStringAsync(str);

            // Checks if such a produit exists
            if (produit.Value == null)
            {
                return NotFound();
            }

            return produit;
        }

        [HttpGet]
        [Route("SummaryById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProduitDto>> GetProduitSummaryById(int id)
        {
            var produit = await dataRepositoryProduitDTO.GetSummaryByIdAsync(id);

            // Check if produit exists
            if (produit.Value == null)
            {
                return NotFound();
            }

            return produit;
        }

        [HttpGet]
        [Route("SummaryByNom/{str}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProduitDto>> GetProduitSummaryByNom(string str)
        {
            var produit = await dataRepositoryProduitDTO.GetSummaryByStringAsync(str);

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
            var produitDtoToUpdate = await dataRepositoryProduitDTO.GetSummaryByIdAsync(id);

            // Check if produit exists
            if (produitDtoToUpdate.Value == null)
            {
                return NotFound();
            }

            // Checks if both ids are the same
            if (id != produit.IdProduit)
            {
                return BadRequest();
            }

            Produit produitToUpdate = mapper.Map<Produit>(produitDtoToUpdate.Value);

            await dataRepository.UpdateAsync(produitToUpdate, produit);
            return Ok(produitDtoToUpdate.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Produit>> PostProduit(Produit produit)
        {
            var existingProduit = await dataRepositoryProduitDTO.GetSummaryByIdAsync(produit.IdProduit);

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
            var produitDto = await dataRepositoryProduitDTO.GetSummaryByIdAsync(id);

            // Checks if product exists
            if (produitDto.Value == null)
            {
                return NotFound();
            }

            Produit produitToDelete = mapper.Map<Produit>(produitDto.Value);

            await dataRepository.DeleteAsync(produitToDelete);
            return Ok(produitDto.Value);
        }
    }
}
