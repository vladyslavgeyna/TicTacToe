using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TicTacToeClassLibrary;

namespace TicTacToeConsole
{
    public class JsonFileConvertorHelper
    {
        private readonly string _filePath;
        public JsonFileConvertorHelper(string filePath)
        {
            _filePath = filePath;
        }
        public void ClearFileContent()
        {
            try
            {
                File.WriteAllText(_filePath, string.Empty);
            }
            catch
            {
                throw new Exception("Something went wrong with clearing file.");
            }
        }
        public void SerializeObjectToFile(object? obj)
        {
            try
            {
                string jsonString = JsonConvert.SerializeObject(obj);
                File.WriteAllText(_filePath, jsonString);
            }
            catch
            {
                throw new Exception("Something went wrong with serializing object to file.");
            }
        }
        public T? DeserializeObjectFromFile<T>()
        {
            try
            {
                T? obj = JsonConvert.DeserializeObject<T>(File.ReadAllText(_filePath));
                return obj;
            }
            catch
            {
                throw new Exception("Something went wrong with deserializing object from file.");
            }
        }
    }
}
