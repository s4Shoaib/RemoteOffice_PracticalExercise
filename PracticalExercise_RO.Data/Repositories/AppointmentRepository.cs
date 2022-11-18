using PracticalExercise_RO.Data.Models;
using PracticalExercise_RO.Data.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PracticalExercise_RO.Data.Repositories
{
    public interface IAppointmentRepository
    {
        List<PractitionerProfitability> GetAppointments(string fromDate, string toDate);
        List<Appointment> SearchAppointments(AppointmentSearch searchModel);
        List<Appointment> GetAppointmentsByPractitionerId(int practitionerId);
    }
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ILoadData _loadData;
        public AppointmentRepository(ILoadData loadData)
        {
            _loadData = loadData ?? throw new ArgumentNullException(nameof(loadData));
        }

        public List<PractitionerProfitability> GetAppointments(string fromDate, string toDate)
        {
            List<PractitionerProfitability> practitionerProfitability = new List<PractitionerProfitability>();
            List<Appointment> appointments = LoadData();

            if (appointments.Count > 0)
            {
                //Filter
                appointments = appointments.Where(x => x.Date >= (!String.IsNullOrEmpty(fromDate) ? Convert.ToDateTime(fromDate) : x.Date)
                                                   && x.Date <= (!String.IsNullOrEmpty(toDate) ? Convert.ToDateTime(toDate) : x.Date)
                                                   ).OrderByDescending(x => x.Date).ToList();

                if (appointments.Count > 0)
                {
                    var groupByPractitioner = (from p in appointments
                                               group p by p.Practitioner_Id into g
                                               select new { Practitioner_Id = g.Key, Appointments = g.ToList() }).ToList();

                    foreach (var practioner in groupByPractitioner)
                    {
                        PractitionerProfitability practitionerProfit = new PractitionerProfitability();
                        practitionerProfit.Practitioner_Id = practioner.Practitioner_Id;
                        practitionerProfit.Practitioner_Name = appointments.Where(x => x.Practitioner_Id == practioner.Practitioner_Id)
                                                               .Select(x => x.Practitioner_Name).FirstOrDefault();

                        var monthlyPractitionerAppoinments = practioner.Appointments
                                                             .GroupBy(x => x.MonthYear)
                                                             .Select(p => new { MonthYear = p.Key, Cost = p.Sum(v => v.Cost), Revenue = p.Sum(v => v.Revenue) })
                                                             .OrderByDescending(x => x.MonthYear)
                                                             .ToList();

                        practitionerProfit.MonthlyProfitability = new List<MonthlyProfitability>();
                        foreach (var monthly in monthlyPractitionerAppoinments)
                        {
                            MonthlyProfitability monthlyProfitability = new MonthlyProfitability();

                            monthlyProfitability.MonthYear = monthly.MonthYear;
                            monthlyProfitability.Cost = monthly.Cost;
                            monthlyProfitability.Revenue = monthly.Revenue;

                            practitionerProfit.MonthlyProfitability.Add(monthlyProfitability);
                        }
                        practitionerProfitability.Add(practitionerProfit);
                    }
                }
            }

            return practitionerProfitability;
        }

        public List<Appointment> SearchAppointments(AppointmentSearch searchModel)
        {
            List<Appointment> appointments = LoadData();
            if (appointments.Count > 0)
            {
                //Filter
                appointments = appointments.Where(x => x.Practitioner_Id == (searchModel.Practitioner_Id != null ? searchModel.Practitioner_Id : x.Practitioner_Id)
                                                   && x.Date >= (!String.IsNullOrEmpty(searchModel.FromDate) ? Convert.ToDateTime(searchModel.FromDate) : x.Date)
                                                   && x.Date <= (!String.IsNullOrEmpty(searchModel.ToDate) ? Convert.ToDateTime(searchModel.ToDate) : x.Date)
                                                   ).OrderByDescending(x => x.Date).ToList();
            }

            return appointments;
        }

        private List<Appointment> LoadData()
        {
            List<Appointment> appointments = new List<Appointment>();
            var practitionersData = _loadData.LoadPractitionersData();
            var appointmentsData = _loadData.LoadAppointmentsData();

            if (practitionersData.Count > 0 && appointmentsData.Count > 0)
            {
                appointments = (from a in appointmentsData
                                join p in practitionersData on a.Practitioner_Id equals p.Id
                                select new Appointment
                                {
                                    Id = a.Id,
                                    Date = a.Date,
                                    Month = a.Date.ToString("MMMM"),
                                    Year = a.Date.Year,
                                    MonthYear = a.Date.ToString("MMMM") + " " + a.Date.Year,
                                    Client_Name = a.Client_Name,
                                    Appointment_Type = a.Appointment_Type,
                                    Duration = a.Duration,
                                    Revenue = a.Revenue,
                                    Cost = a.Cost,
                                    Practitioner_Id = p.Id,
                                    Practitioner_Name = p.Name
                                }).OrderByDescending(x => x.Date).ToList();
            }
            return appointments;
        }

        public List<Appointment> GetAppointmentsByPractitionerId(int practitionerId)
        {
            return new List<Appointment>();
        }
    }
}
