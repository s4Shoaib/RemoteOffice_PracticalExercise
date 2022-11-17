using PracticalExercise_RO.Data.Models;
using PracticalExercise_RO.Data.Repositories;
using PracticalExercise_RO.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PracticalExercise_RO.Library.Services
{
    public interface IAppointmentService
    {
        PractitionerProfitabilityModel GetAppointments(string fromDate, string toDate);
        AppointmentModel SearchAppointments(AppointmentSearch searchModel);
        AppointmentModel GetAppointmentsByPractitionerId(int practitionerId);
    }
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository ?? throw new ArgumentNullException(nameof(appointmentRepository));
        }

        public PractitionerProfitabilityModel GetAppointments(string fromDate, string toDate)
        {
            PractitionerProfitabilityModel practitionerProfitabilityModel = new PractitionerProfitabilityModel();
            practitionerProfitabilityModel.PractitionerProfitabilitys = _appointmentRepository.GetAppointments(fromDate, toDate);
            practitionerProfitabilityModel.TotalCounts = practitionerProfitabilityModel.PractitionerProfitabilitys.Count;
            return practitionerProfitabilityModel;
        }

        public AppointmentModel SearchAppointments(AppointmentSearch searchModel)
        {
            AppointmentModel appointmentModel = new AppointmentModel();
            appointmentModel.Appointments = _appointmentRepository.SearchAppointments(searchModel);
            appointmentModel.TotalCounts = appointmentModel.Appointments.Count;
            return appointmentModel;
        }

        public AppointmentModel GetAppointmentsByPractitionerId(int practitionerId)
        {
            AppointmentModel appointmentModel = new AppointmentModel();
            appointmentModel.Appointments = _appointmentRepository.GetAppointmentsByPractitionerId(practitionerId);
            return appointmentModel;
        }
    }
}
