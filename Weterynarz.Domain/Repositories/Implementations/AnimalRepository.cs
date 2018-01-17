using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Weterynarz.Domain.ContextDb;
using Weterynarz.Domain.EntitiesDb;
using Weterynarz.Domain.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using Weterynarz.Domain.ViewModels.Visit;
using Weterynarz.Domain.ViewModels.Animal;
using Microsoft.EntityFrameworkCore;

namespace Weterynarz.Domain.Repositories.Implementations
{
    public class AnimalRepository : BaseRepository<Animal>, IAnimalRepository
    {
        private IAccountsRepository _accountsRepository;
        private IAnimalTypesRepository _animalTypesRepository;

        public AnimalRepository(ApplicationDbContext db, IAccountsRepository accountsRepository,
            IAnimalTypesRepository animalTypesRepository) : base(db)
        {
            _accountsRepository = accountsRepository;
            _animalTypesRepository = animalTypesRepository;
        }

        public async Task CreateNew(AnimalManageViewModel model)
        {
            Animal animal = new Animal()
            {
                Active = model.Active,
                AnimalDesc = model.Description,
                AnimalTypeId = model.AnimalTypeId,
                BirthDate = model.BirthDay,
                CreationDate = DateTime.Now,
                Deleted = false,
                Name = model.Name,
                OwnerId = model.OwnerId
            };

            await base.InsertAsync(animal);
        }

        public async Task Delete(int id)
        {
            Animal animal = base.GetById(id);
            if(animal != null)
            {
                animal.Deleted = true;

                await base.SaveChangesAsync();
            }
        }

        public async Task Edit(AnimalManageViewModel model)
        {
            Animal animal = base.GetById(model.Id);
            if (animal != null)
            {
                animal.Active = model.Active;
                animal.ModificationDate = DateTime.Now;
                animal.Name = model.Name;
                animal.OwnerId = model.OwnerId;
                animal.AnimalDesc = model.Description;
                animal.AnimalTypeId = model.AnimalTypeId;
                animal.BirthDate = model.BirthDay;

                await base.SaveChangesAsync();
            }
        }

        public AnimalManageViewModel GetCreateNewViewModel()
        {
            AnimalManageViewModel model = new AnimalManageViewModel();

            model.OwnersSelectList = _accountsRepository.GetUsersSelectList();
            model.AnimalTypesSelectList = _animalTypesRepository.GetAnimalTypesSelectList(); ;

            return model;
        }

        public AnimalManageViewModel GetEditViewModel(int id)
        {
            AnimalManageViewModel model = null;

            Animal animal = base.GetById(id);
            if (animal != null)
            {
                model = new AnimalManageViewModel();

                model.Active = animal.Active;
                model.Name = animal.Name;
                model.OwnerId = animal.OwnerId;
                model.OwnersSelectList = _accountsRepository.GetUsersSelectList();
                model.Description = animal.AnimalDesc;
                model.AnimalTypeId = animal.AnimalTypeId;
                model.AnimalTypesSelectList = _animalTypesRepository.GetAnimalTypesSelectList();
                model.BirthDay = animal.BirthDate;
            }

            return model;
        }

        public IQueryable<AnimalIndexViewModel> GetIndexViewModel()
        {
            return base.GetAllNotDeleted().Select(i => new AnimalIndexViewModel
            {
                Id = i.Id,
                Active = i.Active,
                BirthDay = i.BirthDate,
                CreationDate = i.CreationDate,
                Name = i.Name,
                Owner = i.Owner.Name + " " + i.Owner.Surname,
                Type = i.AnimalType.Name
            });
        }

        public IEnumerable<SelectListItem> GetUserAnimalsSelectList(string userId)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "", Text = "-- wybierz --", Disabled = true, Selected = true });

            if(!string.IsNullOrEmpty(userId))
            {
                list = base.GetAllActive().Where(i => i.Owner.Id == userId).Select(i => new SelectListItem {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }).ToList();
            }
            return list.AsEnumerable();
        }

        public async Task<int> InsertFromVisitFormAsync(VisitMakeVisitViewModel model)
        {
            Animal animal = new Animal()
            {
                Active = true,
                AnimalTypeId = model.AnimalTypeId,
                BirthDate = model.AnimalBirthdate,
                OwnerId = model.UserId,
                Name = model.AnimalName,
                CreationDate = DateTime.Now,
            };

            await base.InsertAsync(animal);

            return animal.Id;
        }
    }
}
