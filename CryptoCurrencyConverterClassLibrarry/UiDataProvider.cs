using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using CryptoCurrencyConverterClassLibrarry.Interfaces;
using CryptoCurrencyConverterClassLibrarry.Models;

namespace CryptoCurrencyConverterClassLibrarry
{
    public class UiDataProvider
    {

        private IFiatDataLoader fiatDataLoader;
        private IDataCache dataCache;

        public UiDataProvider()
        {
            fiatDataLoader = new FiatDataLoaderFromDataFixerIO();
        }

        public FiatCurrencyModel RecieveDataAboutFiatCurreny(FiatCurrencyEnum fiatCurrencyEnum)
        {
            //check if data is cached
            // if is, check cached data datetime
            // if not today try to download data
            // if success download cache and return new data
            // if not return last cached data

            return fiatDataLoader.LoadDataAboutFiatCurrency(fiatCurrencyEnum).Result;
        }

    }

}

