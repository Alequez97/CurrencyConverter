using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using CurrencyConverterClassLibrarry.Interfaces;
using CurrencyConverterClassLibrarry.Models;

namespace CurrencyConverterClassLibrarry
{
    /// <summary>
    /// Class that provides data for ui layer
    /// </summary>
    public class UiDataProvider
    {

        private IFiatDataLoader fiatDataLoader;
        private IDataCache<FiatCurrencyModel> dataCache;

        private FiatCurrencyModel cachedFiatCurrencyModel = new FiatCurrencyModel() { Date = new DateTime(1970, 1, 1) };  //To check if something was possible to load 
                                                                                                                          //creating instance with default paramtres
        public UiDataProvider()
        {
            fiatDataLoader = new FiatDataLoaderFromDataFixerIO();
        }

        /// <summary>
        /// Load data about currency exchange rates
        /// </summary>
        /// <param name="fiatCurrencyEnum">Fiat currency enum</param>
        /// <returns>Objects that contains data about currency rates</returns>
        public FiatCurrencyModel RecieveDataAboutFiatCurreny(FiatCurrencyEnum fiatCurrencyEnum)
        {
            dataCache = new DataToFileCache<FiatCurrencyModel>();
            cachedFiatCurrencyModel = new FiatCurrencyModel() { Date = new DateTime(1970, 1, 1) }; //To check if something was possible to load

            string fileName = fiatCurrencyEnum.ToString() + ".txt";

            if (dataCache.DataIsCached(fileName))
            {
                cachedFiatCurrencyModel = dataCache.LoadCachedData(fileName);
                var currentTicks = DateTime.Now.AddDays(-2).Ticks;
                if (cachedFiatCurrencyModel.Date.Ticks > currentTicks)
                {
                    return cachedFiatCurrencyModel;
                }
                else
                {
                    return TryLoadDataFromApi(fiatCurrencyEnum);
                }
            }
            else
            {
                return TryLoadDataFromApi(fiatCurrencyEnum);
            }
        }


        private FiatCurrencyModel TryLoadDataFromApi(FiatCurrencyEnum fiatCurrencyEnum)
        {
            try
            {
                cachedFiatCurrencyModel = fiatDataLoader.LoadDataAboutFiatCurrency(fiatCurrencyEnum).Result;
                dataCache.SaveDataToCache(cachedFiatCurrencyModel, fiatCurrencyEnum.ToString() + ".txt");
                return cachedFiatCurrencyModel;
            }
            catch
            {
                return cachedFiatCurrencyModel;
            }
        }

    }

}

