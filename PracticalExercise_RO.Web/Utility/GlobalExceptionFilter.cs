using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PracticalExercise_RO.Web.Utility
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        //private readonly ILogger _logger;
        //public GlobalExceptionFilter(ILogger logger)
        //{
        //    _logger = logger;
        //}
        public GlobalExceptionFilter()
        {
        }

        public void OnException(ExceptionContext context)
        {
            //This stops bubbling of exception
            context.ExceptionHandled = true;

            //writes error to trace
            //_logger.LogError(context.Exception,"Error Occurred");

            //redirects to Error Page
            context.Result = new RedirectToActionResult("Error", "Home", null);
        }
    }
}
