using ArbetsprovWebApp.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ArbetsprovWebApp.Models
{
    public class HumTempRainClass
    {
        private const string blobURI = "https://sigmaiotexercisetest.blob.core.windows.net/iotbackend/dockan";
        public class HumTempRainObj
        {
            public HumidityClass.HumidityObj humidityObj;
            public TemperatureClass.TemperatureObj temperatureObj;
            public RainfallClass.RainfallObj rainfalllObj;
        }
        public static async Task<string> HumTempRainResponse(string id)
        {
            var blobDataHumTempRain = await FetchDataService.CallBlobAPI(blobURI + "/HumTempRain/" + id + ".csv");
            var outgoingObjAsList = new List<HumTempRainObj>();

            var listofHumTempRains = blobDataHumTempRain.Split("\r\n").ToList();
            foreach (var HumTempRain in listofHumTempRains)
            {
                if (!string.IsNullOrEmpty(HumTempRain))
                {
                    var HumTempRainData = HumTempRain.Split(";");
                    HumTempRainObj outgoingJson = new HumTempRainObj()
                {
                    //PointInTime = HumTempRainData[0],
                    //HumTempRain = HumTempRainData[1]
                };
                outgoingObjAsList.Add(outgoingJson);
            }
        }
        return JsonSerializer.Serialize(outgoingObjAsList);
        }
    }
}
