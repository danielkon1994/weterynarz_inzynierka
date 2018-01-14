using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weterynarz.Basic.Const;
using Weterynarz.Domain.ContextDb;
using Weterynarz.Domain.EntitiesDb;
using Weterynarz.Domain.Repositories.Interfaces;
using Weterynarz.Domain.Services;
using Weterynarz.Domain.ViewModels.Settings;

namespace Weterynarz.Domain.Repositories.Implementations
{
    public class SettingsContentRepository : BaseRepository<SettingsContent>, ISettingsContentRepository
    {
        private IMemoryCacheService _memoryCacheService;

        public SettingsContentRepository(ApplicationDbContext db, IMemoryCacheService memoryCacheService) : base(db)
        {
            _memoryCacheService = memoryCacheService;
        }

        public SettingsContentViewModel GetSettingsContentViewModel()
        {
            var allActiveSettings = base.GetAllActive().Select(i => new SettingsContentItem
            {
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
            var setting = base.GetById(id);
            if (setting != null)
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
            var settings = base.GetById(model.Id);
            if (settings != null)
            {
                settings.Value = model.Value;
                settings.ModificationDate = model.ModificationDate;

                await base.SaveChangesAsync();

                _memoryCacheService.RemoveContentDict(CacheKey.SettingsContentCache);
            }
        }
    }
}
