using Microsoft.TeamFoundation.Build.WebApi;
using System.Threading.Tasks;

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
        bool Connect(string vstsAccountName, string personalAccessToken);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool CheckConnection();

        /// <summary>
        /// 
        /// </summary>
        void Disconnect();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<BuildHttpClient> GetBuildHttpClient();
    }
}
