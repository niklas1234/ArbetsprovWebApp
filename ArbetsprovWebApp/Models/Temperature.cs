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
        public static async Task<string> TemperatureResponse(string id)
        {
            var blobDataTemperature = await FetchDataService.CallBlobAPI(blobURI + "/temperature/" + id + ".csv");
            var outgoingObjAsList = new List<TemperatureObj>();

            var listofTemperatures = blobDataTemperature.Split("\r\n").ToList();
            foreach (var temperature in listofTemperatures)
            {
                if (!string.IsNullOrEmpty(temperature))
                {
                    var temperatureLine = temperature.Split(";");
                    TemperatureObj outgoingJson = new TemperatureObj()
                    {
                        PointInTime = temperatureLine[0],
                        Temperature = temperatureLine[1]
                    };
                    outgoingObjAsList.Add(outgoingJson);
                }
            }
            return JsonSerializer.Serialize(outgoingObjAsList);
        }
    }
}