using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CurrencyConverterClassLibrarry.Models;

namespace CurrencyConverterClassLibrarry.Interfaces
{
    public interface IFiatDataLoader
    {

        Task<FiatCurrencyModel> LoadDataAboutFiatCurrency(FiatCurrencyEnum fiatCurrency);

    }
}
