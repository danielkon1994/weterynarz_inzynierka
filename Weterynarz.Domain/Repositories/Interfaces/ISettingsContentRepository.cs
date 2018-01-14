using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Weterynarz.Domain.EntitiesDb;
using Weterynarz.Domain.ViewModels.Settings;

namespace Weterynarz.Domain.Repositories.Interfaces
{
    public interface ISettingsContentRepository : IBaseRepository<SettingsContent>
    {
        SettingsContentViewModel GetSettingsContentViewModel();
        SettingsContentManageViewModel GetSettingsContentManageViewModel(int id);
        Task SaveSettingsContent(SettingsContentManageViewModel model);
    }
}
