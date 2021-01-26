using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCurrencyConverterClassLibrarry
{
    public interface IFiatDataLoader
    {

        Task<FiatCurrencyModel> LoadDataAboutFiatCurrency(FiatCurrencyEnum fiatCurrency);

    }
}
