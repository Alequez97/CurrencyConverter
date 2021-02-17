using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CurrencyConverterClassLibrarry.Interfaces;

namespace CurrencyConverterClassLibrarry
{
    /// <summary>
    /// Class that saves loaded data to file
    /// If actual data won't be loaded, last possible data will be loaded from file
    /// </summary>
    /// <typeparam name="TCurrencyModel">Currency moodel that need to be cached</typeparam>
    public class DataToFileCache<TCurrencyModel> : IDataCache<TCurrencyModel> where TCurrencyModel : class
    {
        private string _filePathBase;
        private ObjectSerializerDeserializer<TCurrencyModel> serializerDeserializer;
        private TCurrencyModel obj;

        public DataToFileCache()
        {
            _filePathBase = Path.GetTempPath();
            serializerDeserializer = new ObjectSerializerDeserializer<TCurrencyModel>();
        }

        public bool DataIsCached(string fileName)
        {
            string filePath = _filePathBase + fileName;
            if (File.Exists(filePath))
            {
                if (obj == null) obj = serializerDeserializer.DeserializeObject(fileName);

                if (obj != null) return true;
                else return false;
            }
            else
            {
                return false;
            }
        }

        public TCurrencyModel LoadCachedData(string fileName)
        {
            if (obj == null) obj = serializerDeserializer.DeserializeObject(fileName);
            return (obj != null) ? obj : throw new Exception("Can't load data"); 
        }

        public bool SaveDataToCache(TCurrencyModel model, string fileName)
        {
            try
            {
                serializerDeserializer.SerializeObject(model, fileName);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
