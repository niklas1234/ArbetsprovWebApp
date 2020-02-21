using ArbetsprovWebApp.Models;
using ArbetsprovWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json;
using static ArbetsprovWebApp.Models.TemperatureClass;

namespace ArbetsprovWebApp.Controllers
{
    public class DockanController : Controller
    {        
        private string blobDataTemperature1;
        private string blobDataHumidity1;
        private string blobDataRainfall1;
        private string jsongString;
        private const string blobURI = "https://sigmaiotexercisetest.blob.core.windows.net/iotbackend/dockan";

        [HttpGet]
        public async Task<string> DataAsync(string id, string sensorType)
        {
            switch (sensorType)
            {
                case null: //No sensortype -> return all of them
                    var blobDataTemperature = await FetchDataService.CallBlobAPI(blobURI + "/temperature/" + id + ".csv");
                    var blobDataHumidity = await FetchDataService.CallBlobAPI(blobURI + "/humidity/" + id + ".csv");
                    var blobDataRainfall = await FetchDataService.CallBlobAPI(blobURI + "/rainfall/" + id + ".csv");              
                return "Humidity \r\n" + blobDataHumidity + "Rainfall \r\n" + blobDataRainfall + "Temperature \r\n" + blobDataTemperature;
                case "temperature":
                    return await TemperatureClass.TempResponse(id);              
                case "humidity":
                    return await HumidityClass.HumidityResponse(id);                    
                case "rainfall":
                    return await RainfallClass.RainfallResponse(id);
                default:
                    return "Sensortype not found.";
            }            
        }
    }    
}
