using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PracticalExercise_RO.Data.Models;
using PracticalExercise_RO.Library.Models;
using PracticalExercise_RO.Library.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RO_PracticalExercise.Controllers
{
    [Route("api/Appointment")]
    public class AppointmentController : Controller
    {
        private readonly ILogger<AppointmentController> _logger;
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(ILogger<AppointmentController> logger, IAppointmentService appointmentService)
        {
            _logger = logger;
            _appointmentService = appointmentService ?? throw new ArgumentNullException("appointmentService");
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("[action]")]
        public JsonResult Get(string fromDate = "", string toDate = "")
        {
            PractitionerProfitabilityModel practitionerProfitabilityModel = _appointmentService.GetAppointments(fromDate, toDate);
            return Json(practitionerProfitabilityModel);
        }

        [HttpPost("[action]")]
        public JsonResult Search([FromBody] AppointmentSearch searchModel)
        {
            AppointmentModel appointmentModel = _appointmentService.SearchAppointments(searchModel);
            return Json(appointmentModel);
        }

        [HttpGet("[action]/{practitionerId}")]
        public JsonResult GetAppointmentsByPractitionerId(int practitionerId)
        {
            AppointmentModel appointmentModel = _appointmentService.GetAppointmentsByPractitionerId(practitionerId);
            return Json(appointmentModel);
        }
    }
}
