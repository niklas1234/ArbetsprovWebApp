using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ArbetsprovWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;        

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        //[HttpGet]
        //public async Task<IEnumerable<WeatherForecast>> GetAsync()
        //{            
        //    var rng = new Random();
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = rng.Next(-20, 55),
        //        Summary = Summaries[rng.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}

        [HttpGet]
        public async Task<string> GetAsync()
        {
            string blobData = await CallBlobAPI("https://sigmaiotexercisetest.blob.core.windows.net/iotbackend/dockan/humidity/2019-01-10.csv"); //Rätt att kalla på metoden härifrån?
            return blobData;
        }

        //Ska nedan kod ligga i kontollern?
        // HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.
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
