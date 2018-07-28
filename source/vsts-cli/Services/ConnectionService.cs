using McMaster.Extensions.CommandLineUtils;
using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using System;
using System.Threading.Tasks;
using vsx.Extensions;
using vsx.Models;

namespace vsx.Services
{
    public class ConnectionService : IConnectionService
    {
        private readonly ICacheService _cacheService;
        private readonly IConsole _console;
        private VssConnection _vssConnection;

        public ConnectionService(ICacheService cacheService, IConsole console)
        {
            _cacheService = cacheService;
            _console = console;
        }

        public bool Connect(string vstsAccountName, string personalAccessToken)
            => (ValidateCredentials(vstsAccountName, personalAccessToken)) ? ConnectWithCredentials(vstsAccountName, personalAccessToken) : ConnectFromCache();

        public void Disconnect() => _cacheService.ClearCache();

        public async Task<BuildHttpClient> GetBuildHttpClient() => await _vssConnection.GetClientAsync<BuildHttpClient>();

        private bool ValidateCredentials(string vstsAccountName, string personalAccessToken)
        {
            // TODO

            return true;
        }

        private bool ConnectWithCredentials(string vstsAccountName, string personalAccessToken)
        {
            _vssConnection = GetVssConnection(vstsAccountName, personalAccessToken);

            try
            {
                _vssConnection.ConnectAsync().SyncResult();
            }
            catch (Exception ex)
            {
                _console.ErrorMessage(ex.Message);
            }

            if (_vssConnection.HasAuthenticated)
            {
                _cacheService.CacheCredentials(vstsAccountName, personalAccessToken);
                return true;
            }

            return false;
        }

        private bool ConnectFromCache()
        {
            var model = _cacheService.GetModelFromCache<ConnectionModel>();
            return ConnectWithCredentials(model.AccountName, model.PersonalAccessToken);
        }

        private VssConnection GetVssConnection(string vstsAccountName, string personalAccessToken)
        {
            var baseUrl = new Uri(String.Format("https://{0}.visualstudio.com", vstsAccountName));
            var credentials = new VssBasicCredential(string.Empty, personalAccessToken);
            return new VssConnection(baseUrl, credentials);
        }
    }
}
