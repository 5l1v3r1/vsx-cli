using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using vsx.Models;

namespace vsx.Services
{
    public class CacheService : ICacheService
    {
        public void CacheCredentials(string account, string pat)
        {
            var model = new ConnectionModel()
            {
                AccountName = account,
                PersonalAccessToken = pat
            };

            var serializedModel = SerializeToString(model);

            using (StreamWriter file = File.CreateText($@"{Directory.GetCurrentDirectory()}\connection.txt"))
            {
                file.WriteLine(serializedModel);
            }
        }

        public void SaveApplicationSettings(Dictionary<string,string> settings)
        {
            var model = new ApplicationSettingsModel()
            {
                
            };
        }

        public T GetModelFromCache<T>() where T : class, IModel, new() 
            => DeserializeFromString<T>(new T().GetSerializedModel());

        public void ClearCache()
        {
            var file = $@"{Directory.GetCurrentDirectory()}\connection.txt";
            if (File.Exists(file)) File.Delete(file);

            file = $@"{Directory.GetCurrentDirectory()}\settings.txt";
            if (File.Exists(file)) File.Delete(file);
        }

        private T DeserializeFromString<T>(string settings) where T: class
        {
            byte[] b = Convert.FromBase64String(settings);
            using (var stream = new MemoryStream(b))
            {
                var formatter = new BinaryFormatter();
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

        private string SerializeToString<T>(T settings) where T : class
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, settings);
                stream.Flush();
                stream.Position = 0;
                return Convert.ToBase64String(stream.ToArray());
            }
        }
    }
}
