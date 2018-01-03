using System;
using System.Collections.Generic;
using System.Text;
using Weterynarz.Services.ViewModels.Settings;

namespace Weterynarz.Services.Services.Interfaces
{
    public interface ISettingsContentService
    {
        SettingsContentViewModel GetSettingsContentViewModel();
    }
}
