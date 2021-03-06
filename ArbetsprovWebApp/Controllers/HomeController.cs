﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ArbetsprovWebApp.Controllers
{
    
    public class HomeController : Controller
    {
        //Gets executed for URL ....../Home/Index/#
        public async Task<string> IndexAsync()
        {
            string blobData = await CallBlobAPI("https://sigmaiotexercisetest.blob.core.windows.net/iotbackend/dockan/humidity/2019-01-10.csv");
            return blobData;
        }

        static readonly HttpClient client = new HttpClient();
        static async Task<string> CallBlobAPI(string BlobURI)
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

        //......../Home/Kaffe/#
        public IActionResult Kaffe()
        {
            return null;
        }
    }
}
