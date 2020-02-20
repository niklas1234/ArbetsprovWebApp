using System.Net.Http;
using System.Threading.Tasks;

namespace ArbetsprovWebApp.Services
{
    public static class FetchDataService
    {
        static readonly HttpClient client = new HttpClient();
        private static string responseBody;

        public static async Task<string> CallBlobAPI(string BlobURI)
        {
            try
            {
                return responseBody = await client.GetStringAsync(BlobURI);                
            }
            catch (HttpRequestException e)
            {
                return e.ToString();
            }
        }
    }
}
