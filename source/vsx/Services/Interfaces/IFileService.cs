using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace vsx.Services
{
    public interface IFileService
    {
        void SaveToJson(string json);
    }
}
