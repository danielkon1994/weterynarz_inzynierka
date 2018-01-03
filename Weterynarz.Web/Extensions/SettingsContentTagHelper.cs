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

        [HtmlAttributeName("setting-group")]
        public string SettingGroup { get; set; }

        private readonly ApplicationDbContext _context;
        public SettingsContentTagHelper(ApplicationDbContext context)
        {
            _context = context;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;

            //var sb = new StringBuilder();
            //sb.AppendFormat("")
            output.Content.SetContent("test");
        }

        private string GetSettingValue(string settingName, string settingGroup)
        {
            if(!string.IsNullOrEmpty(settingName) && !string.IsNullOrEmpty(settingGroup))
            {
                return _context.SettingsContent.FirstOrDefault(i => i.Name == settingName && i.Group == settingGroup)?.Name;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
