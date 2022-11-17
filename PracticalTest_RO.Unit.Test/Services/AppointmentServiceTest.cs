using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PracticalExercise_RO.Data.Models;
using PracticalExercise_RO.Data.Repositories;
using PracticalExercise_RO.Library.Services;
using System;
using System.Collections.Generic;

namespace PracticalExercise_RO.Unit.Test.Services
{
    [TestClass]
    public class AppointmentServiceTest
    {
        private Mock<IAppointmentRepository> _mockAppointmentRepository;
        private AppointmentService _sut;

        [TestInitialize]
        public void Initialize()
        {
            _mockAppointmentRepository = new Mock<IAppointmentRepository>(MockBehavior.Strict);
            _sut = new AppointmentService(_mockAppointmentRepository.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _mockAppointmentRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_AppointmentRepositoryIsNull_ThrowsArgumentNullException()
        {
            try
            {
                var result = new AppointmentService(null);
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual(e.ParamName, "appointmentRepository");
                throw;
            }
        }

        [TestMethod]
        public void Ctor_ValidArgumentsPassed_Success()
        {
            //Arrange

            //Act
            var result = new AppointmentService(_mockAppointmentRepository.Object);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetAppointments_WhenCalled_ReturnsPractitionerProfitabilityList()
        {
            //Arrange
            List<PractitionerProfitability> practitionerProfitability = new List<PractitionerProfitability>();
            practitionerProfitability.Add(GetPractitionerProfitabilityObj());

            string fromDate = "7/1/2019";
            string toDate = "12/31/2019";

            _mockAppointmentRepository.Setup(x => x.GetAppointments(fromDate, toDate)).Returns(practitionerProfitability);

            //Act
            var result = _sut.GetAppointments(fromDate, toDate);

            //Assert
            _mockAppointmentRepository.Verify(x => x.GetAppointments(fromDate, toDate), Times.Once);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.PractitionerProfitabilitys.Count == 0);
        }

        [TestMethod]
        public void SearchAppointments_WhenCalled_ReturnsPractitionerProfitabilityList()
        {
            //Arrange
            List<Appointment> appointments = new List<Appointment>();
            appointments.Add(GetAppointmentObj());

            var appointmentSearchObject = GetAppointmentSearchObj();

            _mockAppointmentRepository.Setup(x => x.SearchAppointments(appointmentSearchObject)).Returns(appointments);

            //Act
            var result = _sut.SearchAppointments(appointmentSearchObject);

            //Assert
            _mockAppointmentRepository.Verify(x => x.SearchAppointments(appointmentSearchObject), Times.Once);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Appointments.Count == 0);
        }

        private Appointment GetAppointmentObj()
        {
            Appointment appointment = new Appointment() { Id = 1, Appointment_Type = "Type", Client_Name = "Test Client", Date = DateTime.Now, Duration = 30, Month = "December", Year = 2020, Practitioner_Id = 1, Practitioner_Name = "FirstName", Cost = 150, Revenue = 300 };

            return appointment;
        }

        private PractitionerProfitability GetPractitionerProfitabilityObj()
        {
            PractitionerProfitability practitionerProfitability = new PractitionerProfitability() { Practitioner_Id = 1, Practitioner_Name = "First Name" };
            practitionerProfitability.MonthlyProfitability = new List<MonthlyProfitability>();
            MonthlyProfitability monthlyProfitability = new MonthlyProfitability() { Month = "December 2020", Cost = 150, Revenue = 300 };

            practitionerProfitability.MonthlyProfitability.Add(monthlyProfitability);
            return practitionerProfitability;
        }

        private AppointmentSearch GetAppointmentSearchObj()
        {
            AppointmentSearch appointmentSearch = new AppointmentSearch() { Practitioner_Id = 1, FromDate = "7/1/2019", ToDate = "12/31/2019" };

            return appointmentSearch;
        }
    }
}
