using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP01_Révisions.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TP01_Révisions.Models.EntityFramework;
using TP01_Révisions.Models.Repository;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace TP01_Révisions.Controllers.Tests
{
    [TestClass()]
    public class ProduitsControllerTests
    {
        private ProduitsController _controller;
        private Mock<IDataRepository<Produit>> _mockRepo;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = new Mock<IDataRepository<Produit>>();
            _controller = new ProduitsController(_mockRepo.Object);
        }

        [TestMethod]
        public async Task GetProduitById_ReturnsProduit_WhenProduitExists()
        {
            // Arrange
            int testId = 1;
            Produit testProduit = new Produit { IdProduit = testId, NomProduit = "Laptop", IdType = 1, IdMarque = 1, StockReel = 10, StockMin = 5, StockMax = 20 };
            _mockRepo.Setup(repo => repo.GetByIdAsync(testId)).ReturnsAsync(new ActionResult<Produit>(testProduit));

            // Act
            var result = await _controller.GetProduitById(testId);

            // Assert
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(testId, result.Value.IdProduit);
        }

        [TestMethod]
        public async Task GetProduitById_ReturnsNotFound_WhenProduitDoesNotExist()
        {
            // Arrange
            int testId = 1;
            _mockRepo.Setup(repo => repo.GetByIdAsync(testId)).ReturnsAsync(new ActionResult<Produit>((Produit)null));

            // Act
            var result = await _controller.GetProduitById(testId);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetProduitByNom_ReturnsProduit_WhenProduitExists()
        {
            // Arrange
            string testName = "Laptop";
            Produit testProduit = new Produit { IdProduit = 1, NomProduit = testName, IdType = 1, IdMarque = 1, StockReel = 10, StockMin = 5, StockMax = 20 };
            _mockRepo.Setup(repo => repo.GetByStringAsync(testName)).ReturnsAsync(new ActionResult<Produit>(testProduit));

            // Act
            var result = await _controller.GetProduitByNom(testName);

            // Assert
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(testName, result.Value.NomProduit);
        }

        [TestMethod]
        public async Task GetProduitByNom_ReturnsNotFound_WhenProduitDoesNotExist()
        {
            // Arrange
            string testName = "Laptop";
            _mockRepo.Setup(repo => repo.GetByStringAsync(testName)).ReturnsAsync(new ActionResult<Produit>((Produit)null));

            // Act
            var result = await _controller.GetProduitByNom(testName);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PutProduit_ReturnsOk_WhenUpdateIsSuccessful()
        {
            // Arrange
            int testId = 1;
            Produit existingProduit = new Produit { IdProduit = testId, NomProduit = "Laptop", IdType = 1, IdMarque = 1, StockReel = 10, StockMin = 5, StockMax = 20 };
            Produit updatedProduit = new Produit { IdProduit = testId, NomProduit = "Pacarbel", IdType = 1, IdMarque = 1, StockReel = 10, StockMin = 5, StockMax = 20 };
            _mockRepo.Setup(repo => repo.GetByIdAsync(testId)).ReturnsAsync(new ActionResult<Produit>(existingProduit));

            // Act
            var result = await _controller.PutProduit(testId, updatedProduit);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkResult));
        }

        [TestMethod]
        public async Task PutProduit_ReturnsNotFound_WhenProduitDoesNotExist()
        {
            // Arrange
            int testId = 1;
            Produit produit = new Produit { IdProduit = testId, NomProduit = "Laptop", IdType = 1, IdMarque = 1, StockReel = 10, StockMin = 5, StockMax = 20 };
            _mockRepo.Setup(repo => repo.GetByIdAsync(testId)).ReturnsAsync(new ActionResult<Produit>((Produit)null));

            // Act
            var result = await _controller.PutProduit(testId, produit);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PutProduit_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            // Arrange
            int testId = 1;
            Produit produit = new Produit { IdProduit = 2, NomProduit = "Pacarbel", IdType = 1, IdMarque = 1, StockReel = 10, StockMin = 5, StockMax = 20 };
            _mockRepo.Setup(repo => repo.GetByIdAsync(testId)).ReturnsAsync(new ActionResult<Produit>(produit));

            // Act
            var result = await _controller.PutProduit(testId, produit);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task PostProduit_ReturnsBadRequest_WhenProduitIdAlreadyExists()
        {
            // Arrange
            Produit existingProduit = new Produit { IdProduit = 1, NomProduit = "Laptop", IdType = 1, IdMarque = 1, StockReel = 10, StockMin = 5, StockMax = 20 };
            Produit newProduit = new Produit { IdProduit = 1, NomProduit = "Pacarbel", IdType = 1, IdMarque = 1, StockReel = 10, StockMin = 5, StockMax = 20 };
            _mockRepo.Setup(repo => repo.GetByIdAsync(existingProduit.IdProduit)).ReturnsAsync(new ActionResult<Produit>(existingProduit));

            // Act
            var result = await _controller.PostProduit(newProduit);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task PostProduit_ReturnsCreated_WhenProduitIsAddedSuccessfully()
        {
            // Arrange
            Produit produit = new Produit { IdProduit = 1, NomProduit = "Pacarbel", IdType = 1, IdMarque = 1, StockReel = 10, StockMin = 5, StockMax = 20 };
            _mockRepo.Setup(repo => repo.GetByIdAsync(produit.IdProduit)).ReturnsAsync(new ActionResult<Produit>((Produit)null));

            // Act
            var result = await _controller.PostProduit(produit);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(CreatedResult));
            Assert.AreEqual(produit, ((CreatedResult)result.Result).Value);
        }

        [TestMethod]
        public async Task DeleteProduit_ReturnsNotFound_WhenProduitDoesNotExist()
        {
            // Arrange
            int testId = 1;
            _mockRepo.Setup(repo => repo.GetByIdAsync(testId)).ReturnsAsync(new ActionResult<Produit>((Produit)null));

            // Act
            var result = await _controller.DeleteProduit(testId);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DeleteProduit_ReturnsOk_WhenProduitIsDeletedSuccessfully()
        {
            // Arrange
            int testId = 1;
            Produit produit = new Produit { IdProduit = 1, NomProduit = "Laptop", IdType = 1, IdMarque = 1, StockReel = 10, StockMin = 5, StockMax = 20 };
            _mockRepo.Setup(repo => repo.GetByIdAsync(testId)).ReturnsAsync(new ActionResult<Produit>(produit));

            // Act
            var result = await _controller.DeleteProduit(testId);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkResult));
        }
    }
}