using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weterynarz.Domain.ContextDb;

namespace Weterynarz.Web.Extensions
{
    public class CacheManager
    {
        private ApplicationDbContext _context;
        private IMemoryCache _cache;
        private Dictionary<string, string> settings;

        public CacheManager(ApplicationDbContext context, IMemoryCache cache)
        {
            _context = context;

            settings = new Dictionary<string, string>();
        }

        public static void LoadSettings()
        {
            //var allSettings = _context.SettingsContent.Where(i => i.Active && !i.Deleted);
        }
    }
}
