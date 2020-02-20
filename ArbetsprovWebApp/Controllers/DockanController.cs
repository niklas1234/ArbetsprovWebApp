using ArbetsprovWebApp.Models;
using ArbetsprovWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



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
                    var blobDataHumidity = await FetchDataService.CallBlobAPI(blobURI + "/humidity/" + id + ".csv");
                    var blobDataTemperature = await FetchDataService.CallBlobAPI(blobURI + "/temperature/" + id + ".csv");
                    var blobDataRainfall = await FetchDataService.CallBlobAPI(blobURI + "/rainfall/" + id + ".csv");

                    var x = new List<Temperature>();

                    var listofTemperatures = blobDataTemperature.Split("\r\n").ToList();
                    foreach (var temperature in listofTemperatures)
                    {
                        var temperatureData = temperature.Split(";");
                        var pointinTime = temperatureData[0];
                        var temper = temperatureData[1];
                        var temperatureObj = new Temperature();
                        temperatureObj.PointInTime = pointinTime;
                        temperatureObj.TemperatureCelsius = temper;
                        var json = JsonConvert.SerializeObject(temperatureObj);

                        //Newtonsoft.Json.JsonConvert.SerializeObject(new {foo = "bar"})

                        //var obj = JsonConvert.DeserializeObject<Temperature>();
                    }


                    return "Humidity \r\n" + blobDataHumidity + "Rainfall \r\n" + blobDataRainfall + "Temperature \r\n" + blobDataTemperature;
                case "temperature":
                    return blobDataTemperature1 = await FetchDataService.CallBlobAPI(blobURI + "/temperature/" + id + ".csv");
                case "humidity":
                    return blobDataHumidity1 = await FetchDataService.CallBlobAPI(blobURI + "/humidity/" + id + ".csv");
                case "rainfall":
                    return blobDataRainfall1 = await FetchDataService.CallBlobAPI(blobURI + "/rainfall/" + id + ".csv");
                default:
                    return "Sensortype not found.";
            }            
        }
    }    
}
