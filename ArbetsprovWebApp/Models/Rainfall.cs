using ArbetsprovWebApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ArbetsprovWebApp.Models
{
    public class RainfallClass
    {
        private const string blobURI = "https://sigmaiotexercisetest.blob.core.windows.net/iotbackend/dockan";
        public class RainfallObj
        {
            public string PointInTime { get; set; }
            public string Rainfall { get; set; }
        }

        public static async Task<string> RainfallResponse(string id)
        {
            var blobDataRainfall = await FetchDataService.CallBlobAPI(blobURI + "/rainfall/" + id + ".csv");
            var outgoingObjAsList = new List<RainfallObj>();

            var listofRainfalls = blobDataRainfall.Split("\r\n").ToList();
            foreach (var Rainfall in listofRainfalls)
            {
                if (!string.IsNullOrEmpty(Rainfall))
                {
                    var rainfallLine = Rainfall.Split(";");
                    RainfallObj outgoingJson = new RainfallObj()
                    {
                        PointInTime = rainfallLine[0],
                        Rainfall = rainfallLine[1]
                    };
                    outgoingObjAsList.Add(outgoingJson);
                }
            }
            return JsonSerializer.Serialize(outgoingObjAsList);
        }
    }
}