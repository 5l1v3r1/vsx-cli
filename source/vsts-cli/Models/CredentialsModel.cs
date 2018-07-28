using System;

namespace vsx.Models
{
    [Serializable()]
    public class CredentialsModel
    {
        public CredentialsModel(string accountName, string personalAccessToken)
        {
            AccountName = accountName;
            PersonalAccessToken = personalAccessToken;
        }

        public string AccountName { get; set; }

        public string PersonalAccessToken { get; set; }
    }
}
