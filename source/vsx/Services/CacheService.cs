using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using vsx.Models;

namespace vsx.Services
{
    public class CacheService : ICacheService
    {
        private readonly string _connectionPath;
        private readonly string _settingsPath;

        public CacheService()
        {
            _connectionPath = $@"{Directory.GetCurrentDirectory()}\connection.txt";
            _settingsPath = $@"{Directory.GetCurrentDirectory()}\settings.txt";
        }

        public void CacheConnection(CredentialsModel credentialsModel)
        {
            var model = new ConnectionModel()
            {
                Expiration = DateTimeOffset.UtcNow.AddHours(1),
                Credentials = credentialsModel
            };

            var serializedModel = SerializeToString(model);

            using (StreamWriter file = File.CreateText(_connectionPath))
            {
                file.WriteLine(serializedModel);
            }
        }

        public CredentialsModel GetConnection()
        {
            var model = new CredentialsModel(default, default, default);
            var deserializedModelFromCache = DeserializeFromString<ConnectionModel>(File.ReadAllText(_connectionPath));

            if (deserializedModelFromCache.Expiration > DateTimeOffset.UtcNow)
            {
                model.AccountName = deserializedModelFromCache.Credentials.AccountName;
                model.Project = deserializedModelFromCache.Credentials.Project;
                model.PersonalAccessToken = deserializedModelFromCache.Credentials.PersonalAccessToken;
            }
            else
            {
                ClearConnectionCache();
            }

            return model;
        }

        public void ClearConnectionCache()
        {
            if (File.Exists(_connectionPath)) File.Delete(_connectionPath);
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
