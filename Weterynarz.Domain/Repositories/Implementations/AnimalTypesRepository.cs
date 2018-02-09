using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Weterynarz.Domain.ContextDb;
using Weterynarz.Domain.EntitiesDb;
using Weterynarz.Domain.Repositories.Interfaces;
using System.Linq;
using Weterynarz.Domain.ViewModels.AnimalTypes;
using Microsoft.Extensions.Logging;

namespace Weterynarz.Domain.Repositories.Implementations
{
    public class AnimalTypesRepository : BaseRepository<AnimalType>, IAnimalTypesRepository
    {
        private ILogger<AnimalTypesRepository> _logger;

        public AnimalTypesRepository(ApplicationDbContext db, ILogger<AnimalTypesRepository> logger) : base(db)
        {
            _logger = logger;
        }

        public IEnumerable<SelectListItem> GetAnimalTypesSelectList()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            list = base.GetAllActive().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }).ToList();

            return list.AsEnumerable();
        }

        public IQueryable<AnimalTypesIndexViewModel> GetIndexViewModel()
        {
            return base.GetAllNotDeleted().Select(a => new AnimalTypesIndexViewModel
            {
                Id = a.Id,
                Active = a.Active,
                Name = a.Name,
                Description = a.Description,
                CreationDate = a.CreationDate
            });
        }

        public async Task CreateNewType(AnimalTypesManageViewModel model)
        {
            AnimalType type = new AnimalType
            {
                Active = model.Active,
                CreationDate = DateTime.Now,
                Description = model.Description,
                Name = model.Name
            };

            await base.InsertAsync(type);
        }

        public AnimalTypesManageViewModel GetEditViewModel(int id)
        {
            AnimalTypesManageViewModel model;
            var animalType = base.GetById(id);
            if (animalType != null)
            {
                model = new AnimalTypesManageViewModel
                {
                    Active = animalType.Active,
                    Name = animalType.Name,
                    Id = animalType.Id,
                    Description = animalType.Description,
                };

                return model;
            }

            return null;
        }

        public async Task<bool> EditType(AnimalTypesManageViewModel model)
        {
            var animalType = base.GetById(model.Id);
            if (animalType != null)
            {
                animalType.Active = model.Active;
                animalType.Description = model.Description;
                animalType.Name = model.Name;
                animalType.ModificationDate = DateTime.Now;

                await base.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteType(int id)
        {
            var animalType = base.GetById(id);
            if (animalType != null)
            {
                try
                {
                    await base.SoftDeleteAsync(animalType);
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Błąd przy usuwaniu typu zwierzęcia");
                    return false;
                }
            }

            return false;
        }
    }
}
