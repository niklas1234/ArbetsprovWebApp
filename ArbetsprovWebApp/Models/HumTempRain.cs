using System.Threading.Tasks;

namespace ArbetsprovWebApp.Models
{
    public class HumTempRainClass
    {       
        public static async Task<string> HumTempRainResponse(string dateParam)
        {  
            return await HumidityClass.HumidityResponse(dateParam) + 
                TemperatureClass.TemperatureResponse(dateParam) + 
                RainfallClass.RainfallResponse(dateParam);
        }        
    }
}


