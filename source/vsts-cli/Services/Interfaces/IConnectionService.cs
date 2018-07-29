using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.TeamFoundation.DistributedTask.WebApi;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients;
using System.Threading.Tasks;
using vsx.Models;

namespace vsx.Services
{
    /// <summary>
    /// VssConnection wrapper
    /// </summary>
    public interface IConnectionService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="vstsAccountName"></param>
        /// <param name="personalAccessToken"></param>
        bool Connect(CredentialsModel credentialsModel);

        /// <summary>
        /// 
        /// </summary>
        string AccountName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string Project { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string PersonalAccessToken { get; set; }

        /// <summary>
        /// 
        /// </summary>
        void Disconnect();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<BuildHttpClient> GetBuildHttpClient();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<ReleaseHttpClient> GetReleaseHttpClient();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<TaskAgentHttpClient> GetTaskHttpClient();
    }
}
