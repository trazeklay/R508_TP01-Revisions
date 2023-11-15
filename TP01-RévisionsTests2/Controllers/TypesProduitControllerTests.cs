using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP01_Révisions.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using TP01_Révisions.Models.EntityFramework;
using TP01_Révisions.Models.Repository;
using Microsoft.AspNetCore.Mvc;

using TP01_Révisions.Controllers;

namespace TP01_Révisions.Controllers.Tests
{
    [TestClass()]
    public class TypeProduitsControllerTests
    {
        private TypeProduitsController _controller;
        private Mock<IDataRepository<TypeProduit>> _mockRepo;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = new Mock<IDataRepository<TypeProduit>>();
            _controller = new TypeProduitsController(_mockRepo.Object);
        }

        [TestMethod]
        public async Task GetTypeProduitById_ReturnsTypeProduit_WhenTypeProduitExists()
        {
            // Arrange
            int testId = 1;
            TypeProduit testTypeProduit = new TypeProduit { IdTypeProduit = testId, NomTypeProduit = "Electronics" };
            _mockRepo.Setup(repo => repo.GetByIdAsync(testId)).ReturnsAsync(new ActionResult<TypeProduit>(testTypeProduit));

            // Act
            var result = await _controller.GetTypeProduitById(testId);

            // Assert
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(testId, result.Value.IdTypeProduit);
        }

        [TestMethod]
        public async Task GetTypeProduitById_ReturnsNotFound_WhenTypeProduitDoesNotExist()
        {
            // Arrange
            int testId = 1;
            _mockRepo.Setup(repo => repo.GetByIdAsync(testId)).ReturnsAsync(new ActionResult<TypeProduit>((TypeProduit)null));

            // Act
            var result = await _controller.GetTypeProduitById(testId);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetTypeProduitByNom_ReturnsTypeProduit_WhenTypeProduitExists()
        {
            // Arrange
            string testName = "Electronics";
            TypeProduit testTypeProduit = new TypeProduit { IdTypeProduit = 1, NomTypeProduit = testName };
            _mockRepo.Setup(repo => repo.GetByStringAsync(testName)).ReturnsAsync(new ActionResult<TypeProduit>(testTypeProduit));

            // Act
            var result = await _controller.GetTypeProduitByNom(testName);

            // Assert
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(testName, result.Value.NomTypeProduit);
        }

        [TestMethod]
        public async Task GetTypeProduitByNom_ReturnsNotFound_WhenTypeProduitDoesNotExist()
        {
            // Arrange
            string testName = "Electronics";
            _mockRepo.Setup(repo => repo.GetByStringAsync(testName)).ReturnsAsync(new ActionResult<TypeProduit>((TypeProduit)null));

            // Act
            var result = await _controller.GetTypeProduitByNom(testName);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PutTypeProduit_ReturnsOk_WhenUpdateIsSuccessful()
        {
            // Arrange
            int testId = 1;
            TypeProduit existingTypeProduit = new TypeProduit { IdTypeProduit = testId, NomTypeProduit = "Electronics" };
            TypeProduit updatedTypeProduit = new TypeProduit { IdTypeProduit = testId, NomTypeProduit = "Clothing" };
            _mockRepo.Setup(repo => repo.GetByIdAsync(testId)).ReturnsAsync(new ActionResult<TypeProduit>(existingTypeProduit));

            // Act
            var result = await _controller.PutTypeProduit(testId, updatedTypeProduit);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkResult));
        }

        [TestMethod]
        public async Task PutTypeProduit_ReturnsNotFound_WhenTypeProduitDoesNotExist()
        {
            // Arrange
            int testId = 1;
            TypeProduit typeProduit = new TypeProduit { IdTypeProduit = testId, NomTypeProduit = "Electronics" };
            _mockRepo.Setup(repo => repo.GetByIdAsync(testId)).ReturnsAsync(new ActionResult<TypeProduit>((TypeProduit)null));

            // Act
            var result = await _controller.PutTypeProduit(testId, typeProduit);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PutTypeProduit_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            // Arrange
            int testId = 1;
            TypeProduit typeProduit = new TypeProduit { IdTypeProduit = 2, NomTypeProduit = "Electronics" };
            _mockRepo.Setup(repo => repo.GetByIdAsync(testId)).ReturnsAsync(new ActionResult<TypeProduit>(typeProduit));

            // Act
            var result = await _controller.PutTypeProduit(testId, typeProduit);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task PostTypeProduit_ReturnsBadRequest_WhenTypeProduitIdAlreadyExists()
        {
            // Arrange
            TypeProduit existingTypeProduit = new TypeProduit { IdTypeProduit = 1, NomTypeProduit = "Electronics" };
            TypeProduit newTypeProduit = new TypeProduit { IdTypeProduit = 1, NomTypeProduit = "Computers" };
            _mockRepo.Setup(repo => repo.GetByIdAsync(existingTypeProduit.IdTypeProduit)).ReturnsAsync(new ActionResult<TypeProduit>(existingTypeProduit));

            // Act
            var result = await _controller.PostTypeProduit(newTypeProduit);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task PostTypeProduit_ReturnsCreated_WhenTypeProduitIsAddedSuccessfully()
        {
            // Arrange
            TypeProduit typeProduit = new TypeProduit { IdTypeProduit = 2, NomTypeProduit = "Computers" };
            _mockRepo.Setup(repo => repo.GetByIdAsync(typeProduit.IdTypeProduit)).ReturnsAsync(new ActionResult<TypeProduit>((TypeProduit)null));

            // Act
            var result = await _controller.PostTypeProduit(typeProduit);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(CreatedResult));
            Assert.AreEqual(typeProduit, ((CreatedResult)result.Result).Value);
        }

        [TestMethod]
        public async Task DeleteTypeProduit_ReturnsNotFound_WhenTypeProduitDoesNotExist()
        {
            // Arrange
            int testId = 1;
            _mockRepo.Setup(repo => repo.GetByIdAsync(testId)).ReturnsAsync(new ActionResult<TypeProduit>((TypeProduit)null));

            // Act
            var result = await _controller.DeleteTypeProduit(testId);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DeleteTypeProduit_ReturnsOk_WhenTypeProduitIsDeletedSuccessfully()
        {
            // Arrange
            int testId = 1;
            TypeProduit typeProduit = new TypeProduit { IdTypeProduit = testId, NomTypeProduit = "Electronics" };
            _mockRepo.Setup(repo => repo.GetByIdAsync(testId)).ReturnsAsync(new ActionResult<TypeProduit>(typeProduit));

            // Act
            var result = await _controller.DeleteTypeProduit(testId);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkResult));
        }
    }
}