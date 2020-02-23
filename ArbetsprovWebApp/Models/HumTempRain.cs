using System.Threading.Tasks;

namespace ArbetsprovWebApp.Models
{
    public static class HumTempRainClass
    {       
        public static async Task<string> HumTempRainResponse(string dateParam)
        {
            var humidityResp = await HumidityClass.HumidityResponse(dateParam);
            var temperatureResp = await TemperatureClass.TemperatureResponse(dateParam);
            var rainfallResp = await RainfallClass.RainfallResponse(dateParam);

            return humidityResp + temperatureResp + rainfallResp;
        }        
    }
}


