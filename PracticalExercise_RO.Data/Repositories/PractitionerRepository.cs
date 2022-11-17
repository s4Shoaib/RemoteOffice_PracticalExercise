using PracticalExercise_RO.Data.Models;
using PracticalExercise_RO.Data.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace PracticalExercise_RO.Data.Repositories
{
    public interface IPractitionerRepository
    {
        List<Practitioner> GetPractitioners();
        Practitioner GetPractitioner(int id);
    }
    public class PractitionerRepository : IPractitionerRepository
    {
        private readonly ILoadData _loadData;

        public PractitionerRepository(ILoadData loadData)
        {
            _loadData = loadData ?? throw new ArgumentNullException(nameof(loadData));

        }

        public List<Practitioner> GetPractitioners()
        {
            List<Practitioner> practitioners = null;

            practitioners = _loadData.LoadPractitionersData();

            return practitioners;
        }

        public Practitioner GetPractitioner(int id)
        {
            Practitioner practitioner = null;

            var practitioners = _loadData.LoadPractitionersData();
            if (practitioners.Count > 0)
            {
                practitioner = practitioners.Find(x => x.Id == id);
            }

            return practitioner;
        }
    }
}
