using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PracticalExercise_RO.Web.Models;
using Microsoft.AspNetCore.Http;

namespace PracticalExercise_RO.Web.Controllers
{
    //[Authorize("Admin")] //This way we can authorize controller access
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            Response.StatusCode = 500;
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult PageNotFound()
        {
            Response.StatusCode = 404;
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ForbiddenError()
        {
            Response.StatusCode = 403;
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult CheckSite()
        {
            bool siteIsValid = true;

            //Todo: Check DB connection

            if (siteIsValid)
                ViewBag.results = "WEBSITE IS OK";

            return View();
        }
    }
}
