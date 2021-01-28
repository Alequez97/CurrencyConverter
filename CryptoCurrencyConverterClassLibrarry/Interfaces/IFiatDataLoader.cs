using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CryptoCurrencyConverterClassLibrarry.Models;

namespace CryptoCurrencyConverterClassLibrarry.Interfaces
{
    public interface IFiatDataLoader
    {

        Task<FiatCurrencyModel> LoadDataAboutFiatCurrency(FiatCurrencyEnum fiatCurrency);

    }
}
