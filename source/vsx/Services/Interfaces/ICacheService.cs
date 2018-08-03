using System.Collections.Generic;
using vsx.Models;

namespace vsx.Services
{
    public interface ICacheService
    {
        void CacheConnection(CredentialsModel credentialsModel);

        CredentialsModel GetConnection();

        void ClearConnectionCache();

        void CacheApplicationSettings(Dictionary<string, string> settings);

        ApplicationSettingsModel GetApplicationSettings();

        void ClearApplicationSettingsCache();
    }
}
