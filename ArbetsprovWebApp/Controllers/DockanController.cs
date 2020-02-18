using ArbetsprovWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ArbetsprovWebApp.Controllers
{
    public class DockanController : Controller
    {        
        private string blobDataTemperature1;
        private string blobDataHumidity1;
        private string blobDataRainfall1;
        private const string blobURI = "https://sigmaiotexercisetest.blob.core.windows.net/iotbackend/dockan";

        [HttpGet]
        public async Task<string> DataAsync(string id, string sensorType)
        {
            switch (sensorType)
            {
                case null: //No sensortype
                    string blobDataHumidity = await FetchDataService.CallBlobAPI(blobURI + "/humidity/" + id + ".csv");
                    string blobDataTemperature = await FetchDataService.CallBlobAPI(blobURI + "/temperature/" + id + ".csv");
                    string blobDataRainfall = await FetchDataService.CallBlobAPI(blobURI + " /rainfall/ " + id + ".csv");
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
