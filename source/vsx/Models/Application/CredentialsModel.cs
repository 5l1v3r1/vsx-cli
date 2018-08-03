using System;

namespace vsx.Models
{
    [Serializable()]
    public class CredentialsModel
    {
        public CredentialsModel(string accountName, string project, string personalAccessToken)
        {
            AccountName = accountName;
            Project = project;
            PersonalAccessToken = personalAccessToken;
        }

        public string AccountName { get; set; }

        public string Project { get; set; }

        public string PersonalAccessToken { get; set; }
    }
}
