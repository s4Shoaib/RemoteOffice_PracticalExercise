using Newtonsoft.Json;
using PracticalExercise_RO.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PracticalExercise_RO.Data.Utility
{
    public interface ILoadData
    {
        List<Practitioner> LoadPractitionersData();
        List<Appointment> LoadAppointmentsData();
    }
    public class LoadData : ILoadData
    {
        private readonly ICacheService _cacheService;
        public LoadData(ICacheService cacheService)
        {
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
        }

        public List<Practitioner> LoadPractitionersData()
        {
            List<Practitioner> practitioners = null;
            var cacheValue = _cacheService.Get<List<Practitioner>>(Constants.PractitionerKey);
            if (cacheValue == null)
            {
                string data = System.IO.File.ReadAllText(@"practitioners.json");
                practitioners = JsonConvert.DeserializeObject<List<Practitioner>>(data);

                _cacheService.Save(Constants.PractitionerKey, practitioners, Cache.CacheExpirationOffset);
            }
            else
            {
                practitioners = cacheValue as List<Practitioner>;
            }
            return practitioners;
        }

        public List<Appointment> LoadAppointmentsData()
        {
            List<Appointment> appointments = null;
            var cacheValue = _cacheService.Get<List<Appointment>>(Constants.AppointmentKey);
            if (cacheValue == null)
            {

                string data = System.IO.File.ReadAllText(@"appointments.json");
                appointments = JsonConvert.DeserializeObject<List<Appointment>>(data);

                _cacheService.Save(Constants.AppointmentKey, appointments, Cache.CacheExpirationOffset);
            }
            else
            {
                appointments = cacheValue as List<Appointment>;
            }
            return appointments;
        }
    }
}
