using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace TestApiCalls
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }
    }

    public class ApiCall
    {
        private const string url = "http://newsapi.org/v2/everything?q=bitcoin&from=2021-01-13&sortBy=publishedAt&";
        private const string apiKey = "ec854c832feb4639adc98376d007d38f";

        public void GetNewsAboutBitcoin()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(url + apiKey);

            //Adding and accepting headers for JSON format
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //List data response
            var response = httpClient.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                var dataObjects = response.Content.ReadAsAsync<NewsLetter>();
            }
            else
            {

            }
        }
    }
}
