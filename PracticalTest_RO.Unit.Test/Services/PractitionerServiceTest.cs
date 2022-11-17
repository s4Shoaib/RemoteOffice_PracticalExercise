using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PracticalExercise_RO.Data.Models;
using PracticalExercise_RO.Data.Repositories;
using PracticalExercise_RO.Library.Services;
using System;
using System.Collections.Generic;

namespace PracticalExercise_RO.Unit.Tests
{
    [TestClass]
    public class PractitionerServiceTest
    {
        private Mock<IPractitionerRepository> _mockPractitionerRepository;
        private PractitionerService _sut;

        [TestInitialize]
        public void Initialize()
        {
            _mockPractitionerRepository = new Mock<IPractitionerRepository>(MockBehavior.Strict);
            _sut = new PractitionerService(_mockPractitionerRepository.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _mockPractitionerRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_PractitionerRepositoryIsNull_ThrowsArgumentNullException()
        {
            try
            {
                var result = new PractitionerService(null);
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual(e.ParamName, "practitionerRepository");
                throw;
            }
        }

        [TestMethod]
        public void Ctor_ValidArgumentsPassed_Success()
        {
            //Arrange

            //Act
            var result = new PractitionerService(_mockPractitionerRepository.Object);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetPractitioners_WhenCalled_ReturnsPractitionersList()
        {
            //Arrange
            List<Practitioner> practitioners = new List<Practitioner>();
            practitioners.Add(GetPractitionerObj());

            _mockPractitionerRepository.Setup(x => x.GetPractitioners()).Returns(practitioners);

            //Act
            var result = _sut.GetPractitioners();

            //Assert
            _mockPractitionerRepository.Verify(x => x.GetPractitioners(), Times.Once);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Practitioners.Count == 0);
        }

        [TestMethod]
        public void GetPractitioner_WhenCalledWithValidId_ReturnsPlant()
        {
            //Arrange
            int id = 1;
            Practitioner practitioner = GetPractitionerObj();

            _mockPractitionerRepository.Setup(x => x.GetPractitioner(It.IsAny<int>())).Returns(practitioner).Verifiable();

            //Act
            var practitionerObj = _sut.GetPractitioner(id);

            //Assert
            _mockPractitionerRepository.Verify(x => x.GetPractitioner(id), Times.Once);
            Assert.IsNotNull(practitioner);
        }

        [TestMethod]
        public void GetPractitioner_WhenCalledWithInValidId_ReturnsNullPractitioner()
        {
            //Arrange
            int id = 0;
            Practitioner practitioner = null;

            _mockPractitionerRepository.Setup(x => x.GetPractitioner(It.IsAny<int>())).Returns(practitioner).Verifiable();

            //Act
            var Obj = _sut.GetPractitioner(id);

            //Assert
            _mockPractitionerRepository.Verify(x => x.GetPractitioner(id), Times.Once);
            Assert.IsNull(practitioner);
        }

        private Practitioner GetPractitionerObj()
        {
            Practitioner practitioner = new Practitioner() { Id = 1, Name = "FirstName" };
            return practitioner;
        }
    }
}
