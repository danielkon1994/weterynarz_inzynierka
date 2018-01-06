using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weterynarz.Basic.Const;
using Weterynarz.Domain.ContextDb;
using Weterynarz.Services.Services.Interfaces;

namespace Weterynarz.Services.Services.Implementations
{
    public class MemoryCacheService : IMemoryCacheService
    {
        private IMemoryCache _memoryCache;
        private ApplicationDbContext _context;

        public MemoryCacheService(IMemoryCache memoryCache, ApplicationDbContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        public Dictionary<string, string> GetContentToDict(string cacheKey)
        {
            //MemoryCacheEntryOptions memoryCacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(1));
            return _memoryCache.GetOrCreate(cacheKey, cacheEntry => 
            {
                return _context.SettingsContent.Select(t => new { t.Code, t.Value }).ToDictionary(t => t.Code, t => t.Value);
            });
        }

        public void UpdateContent(string contentKey, string contentValue)
        {
            Dictionary<string, string> dict = GetContentToDict(CacheKey.SettingsContentCache);
            if(dict.ContainsKey(contentKey))
            {
                dict[contentKey] = contentValue;
            }
        }

        public void RemoveContentDict(string cacheKey)
        {
            _memoryCache.Remove(cacheKey);
        }
    }
}
