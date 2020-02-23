using ArbetsprovWebApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ArbetsprovWebApp.Models
{
    public class HumidityClass
    {
        private const string blobURI = "https://sigmaiotexercisetest.blob.core.windows.net/iotbackend/dockan";
        public class HumidityObj
        {
            public string PointInTime { get; set; }
            public string Humidity { get; set; }
        }

        public static async Task<string> HumidityResponse(string dateParam)
        {
            var blobDataHumidity = await FetchDataService.CallBlobAPI(blobURI + "/humidity/" + dateParam + ".csv");
            var outgoingObjAsList = new List<HumidityObj>();

            var listofHumidities = blobDataHumidity.Split("\r\n").ToList();
            foreach (var humidity in listofHumidities)
            {
                if (!string.IsNullOrEmpty(humidity))
                {
                    var humidityData = humidity.Split(";");
                    HumidityObj outgoingJson = new HumidityObj()
                    {
                        PointInTime = humidityData[0],
                        Humidity = humidityData[1]
                    };
                    outgoingObjAsList.Add(outgoingJson);
                }
            }
            return JsonSerializer.Serialize(outgoingObjAsList);
        }
    }
}