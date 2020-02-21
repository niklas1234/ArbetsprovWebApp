using ArbetsprovWebApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ArbetsprovWebApp.Models
{
    public class TemperatureClass
    {
        private const string blobURI = "https://sigmaiotexercisetest.blob.core.windows.net/iotbackend/dockan";
        public class TemperatureObj
        {
            public string PointInTime { get; set; }
            public string Temperature { get; set; }
        }
        public static async Task<string> TempResponse(string id)
        {
            var blobDataTemperature1 = await FetchDataService.CallBlobAPI(blobURI + "/temperature/" + id + ".csv");
            var outgoingObjAsList = new List<TemperatureObj>();

            var listofTemperatures = blobDataTemperature1.Split("\r\n").ToList();
            foreach (var temperature in listofTemperatures)
            {
                if (!string.IsNullOrEmpty(temperature))
                {
                    var temperatureData = temperature.Split(";");
                    TemperatureObj outgoingJson = new TemperatureObj()
                    {
                        PointInTime = temperatureData[0],
                        Temperature = temperatureData[1]
                    };
                    outgoingObjAsList.Add(outgoingJson);
                }
            }
            return JsonSerializer.Serialize(outgoingObjAsList);
        }
    }
}