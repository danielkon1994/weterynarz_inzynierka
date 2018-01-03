using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weterynarz.Domain.ContextDb;

namespace Weterynarz.Web.Extensions
{
    [HtmlTargetElement("settings-content")]
    public class SettingsContentTagHelper : TagHelper
    {
        [HtmlAttributeName("setting-name")]
        public string SettingName { get; set; }

        private readonly ApplicationDbContext _context;
        public SettingsContentTagHelper(ApplicationDbContext context)
        {
            _context = context;
        }

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;

            var sb = new StringBuilder();
            sb.AppendFormat("")
        }
    }
}
