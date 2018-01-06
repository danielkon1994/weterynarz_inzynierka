using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weterynarz.Web.Extensions
{
    public static class HtmlExtensions
    {
        public static string GetCacheContent(string contentKey, Dictionary<string, string> dict)
        {
            string value;
            if(!dict.TryGetValue(contentKey, out value))
            {
                value = string.Empty;
            }

            return value;
        }
    }
}
