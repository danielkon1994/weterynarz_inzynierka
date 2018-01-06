using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weterynarz.Basic.Const;
using Weterynarz.Domain.ContextDb;

namespace Weterynarz.Web.Extensions
{
    [HtmlTargetElement("settings-content")]
    public class SettingsContentTagHelper : TagHelper
    {
        [HtmlAttributeName("setting-name")]
        public string SettingName { get; set; }

        [HtmlAttributeName("setting-group")]
        public string SettingGroup { get; set; }

        private readonly ApplicationDbContext _context;
        private IMemoryCache _cache;
        public SettingsContentTagHelper(ApplicationDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;

            //var sb = new StringBuilder();
            //sb.AppendFormat("")
            output.Content.SetContent("test");
        }

        //private string GetSettingValue(string settingName, string settingGroup)
        //{
        //    // Set cache options.
        //    var cacheEntryOptions = new MemoryCacheEntryOptions()
        //        // Keep in cache for this time, reset time if accessed.
        //        .SetSlidingExpiration(TimeSpan.FromSeconds(3));

        //    // Save data in cache.
        //    _cache.Set(CacheKeys.Entry, cacheEntry, cacheEntryOptions);
        //}
    }
}
