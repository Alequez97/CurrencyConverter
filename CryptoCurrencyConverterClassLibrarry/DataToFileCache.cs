using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CryptoCurrencyConverterClassLibrarry.Interfaces;

namespace CryptoCurrencyConverterClassLibrarry
{
    public class DataToFileCache<TModel> : IDataCache<TModel> where TModel : class
    {
        private string _filePathBase;
        private ObjectSerializerDeserializer<TModel> serializerDeserializer;
        private TModel obj;


        public DataToFileCache()
        {
            _filePathBase = Path.GetTempPath();
            serializerDeserializer = new ObjectSerializerDeserializer<TModel>();
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

        public TModel LoadCachedData(string fileName)
        {
            if (obj == null) obj = serializerDeserializer.DeserializeObject(fileName);
            return (obj != null) ? obj : throw new Exception("Can't load data"); 
        }

        public bool SaveDataToCache(TModel model, string fileName)
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
