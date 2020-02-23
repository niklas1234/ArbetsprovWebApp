using ArbetsprovWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ArbetsprovWebApp.Controllers
{
    public class DockanController : Controller
    {        
        private const string blobURI = "https://sigmaiotexercisetest.blob.core.windows.net/iotbackend/dockan";

        [HttpGet]
        public async Task<string> DataAsync(string dateParam, string sensorType)
        {
            switch (sensorType)
            {
                case null: //No sensortype -> return all of them
                    return await HumTempRainClass.HumTempRainResponse(dateParam);
                case "temperature":
                    return await TemperatureClass.TemperatureResponse(dateParam);              
                case "humidity":
                    return await HumidityClass.HumidityResponse(dateParam);                    
                case "rainfall":
                    return await RainfallClass.RainfallResponse(dateParam);
                default:
                    return "Sensortype not found.";
            }            
        }
    }    
}
