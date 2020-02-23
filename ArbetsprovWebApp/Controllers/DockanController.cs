using ArbetsprovWebApp.Models;
using Microsoft.AspNetCore.Mvc;
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
    }    
}
