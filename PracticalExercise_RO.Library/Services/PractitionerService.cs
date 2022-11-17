using PracticalExercise_RO.Data.Repositories;
using PracticalExercise_RO.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PracticalExercise_RO.Library.Services
{
    public interface IPractitionerService
    {
        PractitionerModel GetPractitioners();
        PractitionerModel GetPractitioner(int id);
    }
    public class PractitionerService : IPractitionerService
    {
        private readonly IPractitionerRepository _practitionerRepository;
        public PractitionerService(IPractitionerRepository practitionerRepository)
        {
            _practitionerRepository = practitionerRepository ?? throw new ArgumentNullException(nameof(practitionerRepository));
        }

        public PractitionerModel GetPractitioner(int id)
        {
            PractitionerModel practitionerModel = new PractitionerModel();
            practitionerModel.Practitioner = _practitionerRepository.GetPractitioner(id);
            return practitionerModel;
        }

        public PractitionerModel GetPractitioners()
        {
            PractitionerModel practitionerModel = new PractitionerModel();
            practitionerModel.Practitioners = _practitionerRepository.GetPractitioners();

            return practitionerModel;
        }
    }
}
