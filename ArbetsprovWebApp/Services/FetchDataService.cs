using System.Net.Http;
using System.Threading.Tasks;

namespace ArbetsprovWebApp.Services
{
    public class FetchDataService
    {
        static readonly HttpClient client = new HttpClient();
        public static async Task<string> CallBlobAPI(string BlobURI)
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
