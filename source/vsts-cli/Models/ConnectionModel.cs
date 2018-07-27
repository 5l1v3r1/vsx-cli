using System;
using System.IO;

namespace vsx.Models
{
    [Serializable()]
    public class ConnectionModel : IModel
    {
        public DateTimeOffset Expiration { get; set; }

        public string AccountName { get; set; }

        public string PersonalAccessToken { get; set; }

        public virtual string GetSerializedModel() => File.ReadAllText($@"{Directory.GetCurrentDirectory()}\connection.txt");
    }
}
