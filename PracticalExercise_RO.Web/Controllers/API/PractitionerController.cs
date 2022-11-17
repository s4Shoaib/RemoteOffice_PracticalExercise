using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PracticalExercise_RO.Library.Models;
using PracticalExercise_RO.Library.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RO_PracticalExercise.Controllers
{
    [Route("api/Practitioner")]
    public class PractitionerController : Controller
    {
        private readonly ILogger<PractitionerController> _logger;
        private readonly IPractitionerService _practitionerService;

        public PractitionerController(ILogger<PractitionerController> logger, IPractitionerService practitionerService)
        {
            _logger = logger;
            _practitionerService = practitionerService ?? throw new ArgumentNullException("practitionerService");
        }

        [HttpGet("[action]")]
        public JsonResult Get()
        {
            PractitionerModel practitionerModel = _practitionerService.GetPractitioners();
            return Json(practitionerModel);
        }

        [HttpGet("[action]/{id}")]
        public JsonResult Get(int id)
        {
            PractitionerModel practitionerModel = _practitionerService.GetPractitioner(id);
            return Json(practitionerModel);
        }
    }
}
