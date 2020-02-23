using ArbetsprovWebApp.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using static ArbetsprovWebApp.Models.HumidityClass;
using static ArbetsprovWebApp.Models.RainfallClass;
using static ArbetsprovWebApp.Models.TemperatureClass;

namespace ArbetsprovWebApp.Models
{
    public class HumTempRainClass
    {
        private const string blobURI = "https://sigmaiotexercisetest.blob.core.windows.net/iotbackend/dockan";        

        public static async Task<string> HumTempRainResponse(string id)
        {  
            //Temperature
            var blobDataTemperature = await FetchDataService.CallBlobAPI(blobURI + "/temperature/" + id + ".csv");
            var outgoingTemperatureObjAsList = new List<TemperatureObj>();
            var listofTemperatures = blobDataTemperature.Split("\r\n").ToList();
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
                    outgoingTemperatureObjAsList.Add(outgoingJson);
                }
            }
            
            //Rainfall
            var blobDataRainfall = await FetchDataService.CallBlobAPI(blobURI + "/rainfall/" + id + ".csv");
            var outgoingRainfallObjAsList = new List<RainfallObj>();
            var listofRainfalls = blobDataRainfall.Split("\r\n").ToList();
            foreach (var Rainfall in listofRainfalls)
            {
                if (!string.IsNullOrEmpty(Rainfall))
                {
                    var RainfallData = Rainfall.Split(";");
                    RainfallObj outgoingJson = new RainfallObj()
                    {
                        PointInTime = RainfallData[0],
                        Rainfall = RainfallData[1]
                    };
                    outgoingRainfallObjAsList.Add(outgoingJson);
                }
            }

            //Humidity
            var blobDataHumidity = await FetchDataService.CallBlobAPI(blobURI + "/humidity/" + id + ".csv");
            var outgoingHumidityObjAsList = new List<HumidityObj>();
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
                    outgoingHumidityObjAsList.Add(outgoingJson);
                }
            }

            return JsonSerializer.Serialize(outgoingHumidityObjAsList) + 
                JsonSerializer.Serialize(outgoingTemperatureObjAsList) + 
                JsonSerializer.Serialize(outgoingRainfallObjAsList);
        }        
    }
}


