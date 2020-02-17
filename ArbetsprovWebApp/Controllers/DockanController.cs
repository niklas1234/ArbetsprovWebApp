using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ArbetsprovWebApp.Controllers
{
    public class DockanController : Controller
    {
        static readonly HttpClient client = new HttpClient();
        private string blobDataTemperature1;
        private string blobDataHumidity1;
        private string blobDataRainfall1;

        [HttpGet]
        public async Task<string> DataAsync(string id, string sensorType)
        {
            switch (sensorType)
            {
                case null: //No sensortype
                    string blobDataHumidity = await CallBlobAPI("https://sigmaiotexercisetest.blob.core.windows.net/iotbackend/dockan/humidity/" + id + ".csv");
                    string blobDataTemperature = await CallBlobAPI("https://sigmaiotexercisetest.blob.core.windows.net/iotbackend/dockan/temperature/" + id + ".csv");
                    string blobDataRainfall = await CallBlobAPI("https://sigmaiotexercisetest.blob.core.windows.net/iotbackend/dockan/rainfall/" + id + ".csv");
                    return "Humidity \r\n" + blobDataHumidity + "Rainfall \r\n" + blobDataRainfall + "Temperature \r\n" + blobDataTemperature;                    

                case "temperature":
                    return blobDataTemperature1 = await CallBlobAPI("https://sigmaiotexercisetest.blob.core.windows.net/iotbackend/dockan/temperature/" + id + ".csv");
                case "humidity":
                    return blobDataHumidity1 = await CallBlobAPI("https://sigmaiotexercisetest.blob.core.windows.net/iotbackend/dockan/humidity/" + id + ".csv");
                case "rainfall":
                    return blobDataRainfall1 = await CallBlobAPI("https://sigmaiotexercisetest.blob.core.windows.net/iotbackend/dockan/rainfall/" + id + ".csv");
                default:
                    return "Sensortype not found.";
            }            
        }

        static async Task<string> CallBlobAPI(string BlobURI)
        {
            try
            {
                string responseBody = await client.GetStringAsync(BlobURI);
                return responseBody;
            }
            catch (HttpRequestException e)
            {
                return e.ToString();
            }
        }
    }    
}
