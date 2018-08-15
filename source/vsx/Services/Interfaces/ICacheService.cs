using vsx.Models;

namespace vsx.Services
{
    public interface ICacheService
    {
        void CacheConnection(CredentialsModel credentialsModel);

        CredentialsModel GetConnection();

        void ClearConnectionCache();
    }
}
