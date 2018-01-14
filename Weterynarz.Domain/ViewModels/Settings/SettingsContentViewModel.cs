using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Weterynarz.Basic.Resources;

namespace Weterynarz.Domain.ViewModels.Settings
{
    public class SettingsContentViewModel
    {
        public IEnumerable<SettingsContentItem> ListSettings { get; set; }
    }

    public class SettingsContentItem : BaseViewModel<int>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public string GroupName { get; set; }
    }
}
