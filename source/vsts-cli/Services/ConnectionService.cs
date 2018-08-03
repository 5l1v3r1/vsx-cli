using McMaster.Extensions.CommandLineUtils;
using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.TeamFoundation.DistributedTask.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients;
using Microsoft.VisualStudio.Services.Search.WebApi;
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

        public string AccountName { get; set; }

        public string Project { get; set; }

        public string PersonalAccessToken { get; set; }

        public bool Connect(CredentialsModel credentialsModel)
            => (ValidateCredentials(credentialsModel)) ? ConnectWithCredentials(credentialsModel) : ConnectFromCache();

        public void Disconnect() => _cacheService.ClearConnectionCache();

        public async Task<BuildHttpClient> GetBuildHttpClient() => await _vssConnection.GetClientAsync<BuildHttpClient>();

        public async Task<ReleaseHttpClient> GetReleaseHttpClient() => await _vssConnection.GetClientAsync<ReleaseHttpClient>();

        public async Task<TaskAgentHttpClient> GetTaskHttpClient() => await _vssConnection.GetClientAsync<TaskAgentHttpClient>();

        public async Task<SearchHttpClient> GetSearchHttpClient() => await _vssConnection.GetClientAsync<SearchHttpClient>();

        public async Task<GitHttpClient> GetGitHttpClient() => await _vssConnection.GetClientAsync<GitHttpClient>();

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
                AccountName = credentialsModel.AccountName;
                Project = credentialsModel.Project;
                PersonalAccessToken = credentialsModel.PersonalAccessToken;
                
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
