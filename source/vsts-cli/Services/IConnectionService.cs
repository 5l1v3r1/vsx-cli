namespace vsx.Services
{
    public interface IConnectionService
    {
        void Connect(string serviceCollectionUrl, string personalAccessToken);
    }
}
