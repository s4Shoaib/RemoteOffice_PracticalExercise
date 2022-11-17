
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using PracticalExercise_RO.Data.Utility;

namespace PracticalExercise_RO.Library.Utility
{
    public class ValidateUserMiddleware
    {
        private readonly RequestDelegate _next;
        public ValidateUserMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context, ICacheService cacheService)
        {
            //Here we can code for authentication and authorization
            await _next.Invoke(context);
        }

    }

    public static class MiddlewareExtension
    {
        public static IApplicationBuilder UseValidateUserMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ValidateUserMiddleware>();
        }
    }
}
