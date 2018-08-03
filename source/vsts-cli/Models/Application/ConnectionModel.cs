using System;

namespace vsx.Models
{
    [Serializable()]
    public class ConnectionModel
    {
        public DateTimeOffset Expiration { get; set; }

        public CredentialsModel Credentials { get; set; }
    }
}
