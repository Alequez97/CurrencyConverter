using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyConverterClassLibrarry.Interfaces
{
    public interface IDataCache<TModel>
    {

        bool DataIsCached(string fileName);

        TModel LoadCachedData(string fileName);

        bool SaveDataToCache(TModel model, string fileName);

    }
}
