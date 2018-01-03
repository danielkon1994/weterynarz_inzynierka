using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weterynarz.Domain.Repositories.Interfaces;
using Weterynarz.Services.Services.Interfaces;
using Weterynarz.Services.ViewModels.Settings;

namespace Weterynarz.Services.Services.Implementations
{
    public class SettingsContentService : ISettingsContentService
    {
        private ISettingsContentRepository _settingsContentRepository;

        public SettingsContentService(ISettingsContentRepository settingsContentRepository)
        {
            _settingsContentRepository = settingsContentRepository;
        }

        public SettingsContentViewModel GetSettingsContentViewModel()
        {
            var allActiveSettings = _settingsContentRepository.GetAllActive().Select(i => new SettingsContentItem {
                Name = i.Name,
                Code = i.Code,
                Id = i.Id,
                Description = i.Description,
                GroupName = i.Group,
                Value = i.Value
            });

            SettingsContentViewModel model = new SettingsContentViewModel
            {
                ListSettings = allActiveSettings.AsEnumerable()
            };

            return model;
        }

        public SettingsContentManageViewModel GetSettingsContentManageViewModel(int id)
        {
            var setting = _settingsContentRepository.GetById(id);
            if(setting != null)
            {
                SettingsContentManageViewModel model = new SettingsContentManageViewModel
                {
                    Id = setting.Id,
                    Name = setting.Name,
                    Value = setting.Value
                };

                return model;
            }

            return null;
            
        }

        public async Task SaveSettingsContent(SettingsContentManageViewModel model)
        {
            var settings = _settingsContentRepository.GetById(model.Id);
            if(settings != null)
            {
                settings.Value = model.Value;
                settings.ModificationDate = model.ModificationDate;

                await _settingsContentRepository.SaveChangesAsync();
            }
        }
    }
}
