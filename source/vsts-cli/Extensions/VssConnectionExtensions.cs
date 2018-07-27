using Microsoft.VisualStudio.Services.WebApi;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using vsx.Models;

namespace vsx.Extensions
{
    public static class VssConnectionExtensions
    {
        public static void SaveToCache(this VssConnection vssConnection, string account, string pat)
        {
            var cacheLocation = $@"{Directory.GetCurrentDirectory()}\cache";
            Directory.CreateDirectory(cacheLocation);

            var model = new ConnectionModel()
            {
                AccountName = account,
                PersonalAccessToken = pat
            };

            var serializedModel = SerializeToString(model);

            using (StreamWriter file = File.CreateText($@"{cacheLocation}\connection.txt"))
            {
                file.WriteLine(serializedModel);
            }
        }

        public static ConnectionModel ReadFromCache(this VssConnection vssConnection)
        {
            var cacheLocation = $@"{Directory.GetCurrentDirectory()}\cache";
            string serializedModel = File.ReadAllText($@"{cacheLocation}\connection.txt");
            return DeserializeFromString<ConnectionModel>(serializedModel);
        }

        public static void ClearCache(this VssConnection vssConnection)
        {
            var fileLocation = $@"{Directory.GetCurrentDirectory()}\cache\connection.txt";
            File.Delete(fileLocation);
        }

        private static T DeserializeFromString<T>(string settings) where T : ConnectionModel
        {
            byte[] b = Convert.FromBase64String(settings);
            using (var stream = new MemoryStream(b))
            {
                var formatter = new BinaryFormatter();
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

        private static string SerializeToString<T>(T settings) where T : ConnectionModel
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
