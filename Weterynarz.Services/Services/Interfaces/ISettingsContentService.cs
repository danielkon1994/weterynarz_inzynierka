using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Weterynarz.Services.ViewModels.Settings;

namespace Weterynarz.Services.Services.Interfaces
{
    public interface ISettingsContentService
    {
        SettingsContentViewModel GetSettingsContentViewModel();
        SettingsContentManageViewModel GetSettingsContentManageViewModel(int id);
        Task SaveSettingsContent(SettingsContentManageViewModel model);
    }
}
