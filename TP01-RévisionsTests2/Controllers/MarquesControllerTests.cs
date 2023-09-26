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

namespace TP01_Révisions.Controllers.Tests
{
    [TestClass()]
    public class MarquesControllerTests
    {
        private MarquesController _controller;
        private TP01DbContext context;
        private Mock<IDataRepository<Marque>> _mockRepo;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = new Mock<IDataRepository<Marque>>();
            _controller = new MarquesController(_mockRepo.Object);
        }

        [TestMethod]
        public async Task GetMarqueById_ReturnsMarque_WhenMarqueExists()
        {
            // Arrange
            int testId = 1;
            Marque testMarque = new Marque { IdMarque = testId, NomMarque = "Aries Corp" };
            _mockRepo.Setup(repo => repo.GetByIdAsync(testId)).ReturnsAsync(new ActionResult<Marque>(testMarque));

            // Act
            var result = await _controller.GetMarqueById(testId);

            // Assert
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(testId, result.Value.IdMarque);
        }

        [TestMethod]
        public async Task GetMarqueById_ReturnsNotFound_WhenMarqueDoesNotExist()
        {
            // Arrange
            int testId = 1;
            _mockRepo.Setup(repo => repo.GetByIdAsync(testId)).ReturnsAsync(new ActionResult<Marque>((Marque)null));

            // Act
            var result = await _controller.GetMarqueById(testId);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetMarqueByNom_ReturnsMarque_WhenMarqueExists()
        {
            // Arrange
            string testName = "Aries Corp";
            Marque testMarque = new Marque { IdMarque = 1, NomMarque = testName };
            _mockRepo.Setup(repo => repo.GetByStringAsync(testName)).ReturnsAsync(new ActionResult<Marque>(testMarque));

            // Act
            var result = await _controller.GetMarqueByNom(testName);

            // Assert
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(testName, result.Value.NomMarque);
        }

        [TestMethod]
        public async Task GetMarqueByNom_ReturnsNotFound_WhenMarqueDoesNotExist()
        {
            // Arrange
            string testName = "Aries Corp";
            _mockRepo.Setup(repo => repo.GetByStringAsync(testName)).ReturnsAsync(new ActionResult<Marque>((Marque)null));

            // Act
            var result = await _controller.GetMarqueByNom(testName);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PutMarque_ReturnsNotFound_WhenMarqueDoesNotExist()
        {
            // Arrange
            int testId = 1;
            Marque marque = new Marque { IdMarque = testId, NomMarque = "Aries Corp" };
            _mockRepo.Setup(repo => repo.GetByIdAsync(testId)).ReturnsAsync(new ActionResult<Marque>((Marque)null));

            // Act
            var result = await _controller.PutMarque(testId, marque);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PutMarque_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            // Arrange
            int testId = 1;
            Marque existingMarque = new Marque { IdMarque = testId, NomMarque = "ExistingMarque" };
            Marque updatedMarque = new Marque { IdMarque = 2, NomMarque = "UpdatedMarque" };
            _mockRepo.Setup(repo => repo.GetByIdAsync(testId)).ReturnsAsync(new ActionResult<Marque>(existingMarque));

            // Act
            var result = await _controller.PutMarque(testId, updatedMarque);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task PutMarque_ReturnsOk_WhenMarqueIsUpdatedSuccessfully()
        {
            // Arrange
            int testId = 1;
            Marque existingMarque = new Marque { IdMarque = testId, NomMarque = "ExistingMarque" };
            Marque updatedMarque = new Marque { IdMarque = testId, NomMarque = "UpdatedMarque" };
            _mockRepo.Setup(repo => repo.GetByIdAsync(testId)).ReturnsAsync(new ActionResult<Marque>(existingMarque));

            // Act
            var result = await _controller.PutMarque(testId, updatedMarque);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkResult));
        }

        [TestMethod]
        public async Task PostMarque_ReturnsBadRequest_WhenIdAlreadyExists()
        {
            // Arrange
            Marque existingMarque = new Marque { IdMarque = 1, NomMarque = "ExistingMarque" };
            Marque newMarqueWithSameId = new Marque { IdMarque = 1, NomMarque = "Aries Corp" };

            // Setting up the mock repository to return the existingMarque when queried by ID
            _mockRepo.Setup(repo => repo.GetByIdAsync(existingMarque.IdMarque)).ReturnsAsync(new ActionResult<Marque>(existingMarque));

            // Act
            var result = await _controller.PostMarque(newMarqueWithSameId);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task PostMarque_ReturnsCreated_WhenMarqueIsAddedSuccessfully()
        {
            // Arrange
            Marque marque = new Marque { IdMarque = 2, NomMarque = "Aries Corp" };
            _mockRepo.Setup(repo => repo.GetByIdAsync(marque.IdMarque)).ReturnsAsync(new ActionResult<Marque>((Marque)null));

            // Act
            var result = await _controller.PostMarque(marque);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(CreatedResult));
            Assert.AreEqual(marque, ((CreatedResult)result.Result).Value);
        }

        [TestMethod]
        public async Task DeleteMarque_ReturnsNotFound_WhenMarqueDoesNotExist()
        {
            // Arrange
            int testId = 1;
            _mockRepo.Setup(repo => repo.GetByIdAsync(testId)).ReturnsAsync(new ActionResult<Marque>((Marque)null));

            // Act
            var result = await _controller.DeleteMarque(testId);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DeleteMarque_ReturnsOk_WhenMarqueIsDeletedSuccessfully()
        {
            // Arrange
            int testId = 1;
            Marque marque = new Marque { IdMarque = testId, NomMarque = "Aries Corp" };
            _mockRepo.Setup(repo => repo.GetByIdAsync(testId)).ReturnsAsync(new ActionResult<Marque>(marque));

            // Act
            var result = await _controller.DeleteMarque(testId);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkResult));
        }

    }
}