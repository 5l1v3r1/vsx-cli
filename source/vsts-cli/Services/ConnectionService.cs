using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using System;
using System.Threading.Tasks;

namespace vsx.Services
{
    public class ConnectionService : IConnectionService
    {
        private VssConnection _vssConnection;

        public bool CheckConnection() => _vssConnection.HasAuthenticated;

        public bool Connect(string vstsAccountName, string personalAccessToken)
        {
            var baseUrl = new Uri(String.Format("https://{0}.visualstudio.com", vstsAccountName));
            var credentials = new VssBasicCredential(string.Empty, personalAccessToken);
            _vssConnection = new VssConnection(baseUrl, credentials);

            _vssConnection.ConnectAsync().SyncResult();

            return (_vssConnection.HasAuthenticated) ? true : false;
        }

        public void Disconnect() => _vssConnection.Disconnect();

        public async Task<BuildHttpClient> GetBuildHttpClient() => await _vssConnection.GetClientAsync<BuildHttpClient>();
    }
}
