using PracticalExercise_RO.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PracticalExercise_RO.Library.Models
{
    public class AppointmentModel
    {
        public List<Appointment> Appointments { get; set; }
        public int TotalCounts { get; set; }
    }
}
