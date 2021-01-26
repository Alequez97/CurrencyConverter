using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CryptoCurrencyConverterClassLibrarry
{
    public class FiatDataLoaderFromDataFixerIO
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
            using (var sr = new StreamWriter("urls.txt", true, Encoding.UTF8))
            {
                sr.WriteLine(url);
            }


            using (HttpResponseMessage response = await httpClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;

                    var convertedData = JsonConvert.DeserializeObject<FiatCurrencyModel>(data);
                    return convertedData;

                    //string fileName = baseCurrency.ToString() + ".txt";
                    //var serializer = new ObjectSerializerDeserializer<FiatCurrencyModel>(_filePathBase + fileName);
                    //serializer.SerializeObject(convertedData);
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

    }
}
