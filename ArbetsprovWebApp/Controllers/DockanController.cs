using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ArbetsprovWebApp.Controllers
{
    public class DockanController : Controller
    {        public async Task<string> DataAsync(string id)
        {
            string blobDataHumidity = await CallBlobAPI("https://sigmaiotexercisetest.blob.core.windows.net/iotbackend/dockan/humidity/" + id + ".csv");
            string blobDataTemperature = await CallBlobAPI("https://sigmaiotexercisetest.blob.core.windows.net/iotbackend/dockan/temperature/" + id + ".csv");
            string blobDataRainfall = await CallBlobAPI("https://sigmaiotexercisetest.blob.core.windows.net/iotbackend/dockan/rainfall/" + id + ".csv");

            string blobData = "Humidity \r\n" + blobDataHumidity + "Rainfall \r\n" + blobDataRainfall + "Temperature \r\n" + blobDataTemperature;

            return blobData;
        }

        static readonly HttpClient client = new HttpClient();
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
