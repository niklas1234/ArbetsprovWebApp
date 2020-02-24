using ArbetsprovWebApp.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ArbetsprovWebApp.Controllers
{
    public class DockanController : Controller
    {
        [HttpGet]
        public async Task<string> DataAsync(string dateParam, string sensorType)
        {
            return sensorType switch
            {
                null => await HumTempRainClass.HumTempRainResponse(dateParam),
                "temperature" => await TemperatureClass.TemperatureResponse(dateParam),
                "humidity" => await HumidityClass.HumidityResponse(dateParam),
                "rainfall" => await RainfallClass.RainfallResponse(dateParam),
                _ => "Sensortype not found.",
            };
        }

        [ApiController]
        public class ErrorController : ControllerBase
        {
            [Route("/error-local-development")]
            public IActionResult ErrorLocalDevelopment([FromServices] IWebHostEnvironment webHostEnvironment)
            {
                if (webHostEnvironment.EnvironmentName != "Development")
                {
                    throw new InvalidOperationException(
                        "This shouldn't be invoked in non-development environments.");
                }

                var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

                return Problem(
                    detail: context.Error.StackTrace,
                    title: context.Error.Message);
            }

            [Route("/error")]
            public IActionResult Error() => Problem();
        }
    }
}
