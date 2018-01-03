using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Weterynarz.Services.ViewModels.Settings
{
    public class SettingsContentManageViewModel : BaseViewModel<int>
    {
        public string Name { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
