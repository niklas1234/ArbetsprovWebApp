using ArbetsprovWebApp.Models;
using ArbetsprovWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json;


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
                    //return await TemperatureClass.TempResponse(id);               
                    //return await HumidityClass.HumidityResponse(id);                
                    //return await RainfallClass.RainfallResponse(id);
                return await HumTempRainClass.HumTempRainResponse(dateParam);
                //var blobDataTemperature = await FetchDataService.CallBlobAPI(blobURI + "/temperature/" + id + ".csv");
                //var blobDataHumidity = await FetchDataService.CallBlobAPI(blobURI + "/humidity/" + id + ".csv");
                //var blobDataRainfall = await FetchDataService.CallBlobAPI(blobURI + "/rainfall/" + id + ".csv");              
                //return "Humidity \r\n" + blobDataHumidity + "Rainfall \r\n" + blobDataRainfall + "Temperature \r\n" + blobDataTemperature;
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
