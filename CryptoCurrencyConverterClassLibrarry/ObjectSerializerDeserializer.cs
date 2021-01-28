using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CryptoCurrencyConverterClassLibrarry
{
    /// <summary>
    /// Provides serialization and deserialization for any type of objects.
    /// Takes file path in constuctor
    /// </summary>
    public class ObjectSerializerDeserializer<T> where T : class
    {

        private string _filePathBase;
        private BinaryFormatter formatter;

        public ObjectSerializerDeserializer()
        {
            _filePathBase = Path.GetTempPath();
            formatter = new BinaryFormatter();
        }

        /// <summary>
        /// Serializes object of type T
        /// </summary>
        /// <param name="obj">The <see cref="T"/> instance that represents the object to serialize</param>
        /// <return>True is object is successfully serialized</return>
        public bool SerializeObject(T obj, string fileName)
        {
            string filePath = _filePathBase + fileName;
            try
            {
                FileStream fileWriteStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                formatter.Serialize(fileWriteStream, obj);
                fileWriteStream.Close();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Deserializes object of type T
        /// </summary>
        /// <return>Object of type T if no exception was found</return>
        public T DeserializeObject(string fileName)
        {
            string filePath = _filePathBase + fileName;

            if (File.Exists(filePath))
            {
                try
                {
                    FileStream fileReadStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    T obj = (T)formatter.Deserialize(fileReadStream);
                    fileReadStream.Close();
                    return obj;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            else
            {
                throw new FileNotFoundException("File " + filePath + " not found");
            }
        }

    }
}
