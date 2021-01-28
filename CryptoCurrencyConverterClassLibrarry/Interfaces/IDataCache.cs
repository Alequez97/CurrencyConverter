using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoCurrencyConverterClassLibrarry.Interfaces
{
    public interface IDataCache<TModel> where TModel : class
    {

        bool CheckIfDataIsCached();

        TModel LoadCachedData();

        bool SaveDataToCache(TModel model);

    }
}
