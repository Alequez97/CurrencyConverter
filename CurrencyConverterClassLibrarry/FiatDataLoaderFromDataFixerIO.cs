using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CurrencyConverterClassLibrarry.Interfaces;
using CurrencyConverterClassLibrarry.Models;

namespace CurrencyConverterClassLibrarry
{
    /// <summary>
    /// Class that loads data about fiat currency rates
    /// </summary>
    public class FiatDataLoaderFromDataFixerIO : IFiatDataLoader
    {

        private readonly string urlBase = "https://api.exchangeratesapi.io/latest?";

        private readonly string _filePathBase = Path.GetTempPath();

        private HttpClient httpClient;

        public FiatDataLoaderFromDataFixerIO()
        {
            httpClient = ApiHelperClass.GetHttpClient();
        }

        public async Task<FiatCurrencyModel> LoadDataAboutFiatCurrency(FiatCurrencyEnum baseCurrency)
        {
            string url = urlBase + "base=" + baseCurrency.ToString().Trim();

            using (HttpResponseMessage response = await httpClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;

                    var convertedData = JsonConvert.DeserializeObject<FiatCurrencyModel>(data);
                    return convertedData;

                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

    }
}
