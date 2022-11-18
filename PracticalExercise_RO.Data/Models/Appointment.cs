using System;
using System.Collections.Generic;
using System.Text;

namespace PracticalExercise_RO.Data.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Month { get; set; }
        public int Year { get; set; }
        public string MonthYear { get; set; }
        public string DateString { get; set; }
        public string Client_Name { get; set; }
        public string Appointment_Type { get; set; }
        public int Duration { get; set; }
        public int Revenue { get; set; }
        public int Cost { get; set; }
        public int Practitioner_Id { get; set; }
        public string Practitioner_Name { get; set; }
    }

    public class AppointmentSearch
    {
        public int? Practitioner_Id { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}
