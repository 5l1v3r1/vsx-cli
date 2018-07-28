using McMaster.Extensions.CommandLineUtils;
using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using System;
using System.Threading.Tasks;
using vsx.Extensions;
using vsx.Models;
using vsx.Validators;

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

        public bool Connect(CredentialsModel credentialsModel)
            => (ValidateCredentials(credentialsModel)) ? ConnectWithCredentials(credentialsModel) : ConnectFromCache();

        public void Disconnect() => _cacheService.ClearConnectionCache();

        public async Task<BuildHttpClient> GetBuildHttpClient() => await _vssConnection.GetClientAsync<BuildHttpClient>();

        private bool ValidateCredentials(CredentialsModel credentialsModel)
        {
            CredentialsModelValidator validator = new CredentialsModelValidator();
            FluentValidation.Results.ValidationResult result = validator.Validate(credentialsModel);
            return result.IsValid;
        }

        private bool ConnectWithCredentials(CredentialsModel credentialsModel)
        {
            _vssConnection = GetVssConnection(credentialsModel);

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
                _cacheService.CacheConnection(credentialsModel);
                return true;
            }

            return false;
        }

        private bool ConnectFromCache()
        {
            var model = _cacheService.GetConnection();
            return ConnectWithCredentials(model);
        }

        private VssConnection GetVssConnection(CredentialsModel credentialsModel)
        {
            var baseUrl = new Uri(String.Format("https://{0}.visualstudio.com", credentialsModel.AccountName));
            var credentials = new VssBasicCredential(string.Empty, credentialsModel.PersonalAccessToken);
            return new VssConnection(baseUrl, credentials);
        }
    }
}
