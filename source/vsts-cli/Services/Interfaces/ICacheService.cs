using System.Collections.Generic;
using vsx.Models;

namespace vsx.Services
{
    public interface ICacheService
    {
        void CacheCredentials(string account, string pat);

        void SaveApplicationSettings(Dictionary<string, string> settings);

        void ClearCache();

        T GetModelFromCache<T>() where T : class, IModel, new();
    }
}
