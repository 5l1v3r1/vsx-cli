using System;
using System.Collections.Generic;
using System.Text;

namespace vsx.Services
{
    public class SettingsService : ISettingsService
    {
        private ICacheService _cacheService;

        public SettingsService(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }
    }
}
