using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weterynarz.Domain.EntitiesDb;
using Weterynarz.Domain.Repositories.Interfaces;
using Weterynarz.Services.Services.Interfaces;
using Weterynarz.Services.ViewModels.AnimalTypes;

namespace Weterynarz.Services.Services.Implementations
{
    public class AnimalTypesService : IAnimalTypesService
    {
        public IAnimalTypesRepository _animalTypesRepository;

        public AnimalTypesService(IAnimalTypesRepository animalTypesRepository)
        {
            this._animalTypesRepository = animalTypesRepository;
        }

        public IQueryable<AnimalTypesIndexViewModel> GetIndexViewModel()
        {
            return _animalTypesRepository.GetAllNotDeleted().Select(a => new AnimalTypesIndexViewModel
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

            await _animalTypesRepository.InsertAsync(type);
        }

        public AnimalTypesManageViewModel GetEditViewModel(int id)
        {
            AnimalTypesManageViewModel model;
            var animalType = _animalTypesRepository.GetById(id);
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
            var animalType = _animalTypesRepository.GetById(model.Id);
            if (animalType != null)
            {
                animalType.Active = model.Active;
                animalType.Description = model.Description;
                animalType.Name = model.Name;
                animalType.ModificationDate = DateTime.Now;

                await _animalTypesRepository.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteType(int id)
        {
            var animalType = _animalTypesRepository.GetById(id);
            if(animalType != null)
            { 
                try
                {
                    await _animalTypesRepository.SoftDeleteAsync(animalType);
                    return true;
                }
                catch(Exception ex)
                {
                    return false;
                }                
            }

            return false;
        }
    }
}
