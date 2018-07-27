using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using System;
using System.Threading.Tasks;
using vsx.Models;

namespace vsx.Services
{
    public class ConnectionService : IConnectionService
    {
        private readonly ICacheService _cacheService;

        public ConnectionService(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public bool Connect(string vstsAccountName, string personalAccessToken)
        {
            var connection = GetVssConnection(vstsAccountName, personalAccessToken);
            connection.ConnectAsync().SyncResult();

            if (connection.HasAuthenticated)
            {
                _cacheService.CacheCredentials(vstsAccountName, personalAccessToken);
                return true;
            }

            return false;
        }

        public void Disconnect() => _cacheService.ClearCache();

        public async Task<BuildHttpClient> GetBuildHttpClient()
        {
            var model = _cacheService.GetModelFromCache<ConnectionModel>();

            var connection = GetVssConnection(model.AccountName, model.PersonalAccessToken);

            return await connection.GetClientAsync<BuildHttpClient>();
        }

        private VssConnection GetVssConnection(string vstsAccountName, string personalAccessToken)
        {
            var baseUrl = new Uri(String.Format("https://{0}.visualstudio.com", vstsAccountName));
            var credentials = new VssBasicCredential(string.Empty, personalAccessToken);
            return new VssConnection(baseUrl, credentials);
        }
    }
}
