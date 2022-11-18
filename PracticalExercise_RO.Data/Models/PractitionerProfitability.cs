using System;
using System.Collections.Generic;
using System.Text;

namespace PracticalExercise_RO.Data.Models
{
    public class PractitionerProfitability
    {
        public int Practitioner_Id { get; set; }
        public string Practitioner_Name { get; set; }
        public List<MonthlyProfitability> MonthlyProfitability { get; set; }
    }

    public class MonthlyProfitability
    {
        public string MonthYear { get; set; }
        public int Cost { get; set; }
        public int Revenue { get; set; }
    }
}
