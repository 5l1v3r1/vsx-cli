using System.IO;

namespace vsx.Services
{
    public class FileService : IFileService
    {
        public void SaveToJson(string json)
        {
            var path = $@"{Directory.GetCurrentDirectory()}\results";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            File.WriteAllText($@"{path}\result.json", json);
        }
    }
}
