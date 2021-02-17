using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CurrencyConverterClassLibrarry
{

    /// <summary>
    /// Class that gives httpClient class instance
    /// </summary>
    public sealed class ApiHelperClass
    {

        private static HttpClient httpClient;

        // Lock synchronization object
        private static object syncLock = new object();

        private ApiHelperClass()
        {

        }

        public static HttpClient GetHttpClient()
        {
            if (httpClient == null)
            {
                lock (syncLock)
                {
                    if (httpClient == null)
                    {
                        httpClient = new HttpClient();
                        httpClient.DefaultRequestHeaders.Accept.Clear();
                        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    }
                }
            }
            return httpClient;
        }
    }
}
