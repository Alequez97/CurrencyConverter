using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace CryptoCurrencyConverterClassLibrarry
{
    public class UiDataProvider
    {

        private FiatDataLoaderFromDataFixerIO fiatDataLoader;

        public UiDataProvider()
        {
            fiatDataLoader = new FiatDataLoaderFromDataFixerIO();
        }

        public FiatCurrencyModel RecieveDataAboutFiatCurreny(FiatCurrencyEnum fiatCurrencyEnum)
        {
            return fiatDataLoader.LoadDataAboutFiatCurrency(fiatCurrencyEnum).Result;
        }

    }

}

