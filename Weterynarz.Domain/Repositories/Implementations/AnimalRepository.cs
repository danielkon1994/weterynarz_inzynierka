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
using System.Diagnostics;

namespace Weterynarz.Domain.Repositories.Implementations
{
    public class AnimalRepository : BaseRepository<Animal>, IAnimalRepository
    {
        private IAccountsRepository _accountsRepository;
        private IAnimalTypesRepository _animalTypesRepository;
        private IDiseasesRepository _diseasesRepository;

        public AnimalRepository(ApplicationDbContext db, IAccountsRepository accountsRepository,
            IAnimalTypesRepository animalTypesRepository, IDiseasesRepository diseasesRepository) : base(db)
        {
            _accountsRepository = accountsRepository;
            _animalTypesRepository = animalTypesRepository;
            _diseasesRepository = diseasesRepository;
        }

        public Animal GetById(int? id)
        {
            return _db.Animals.Where(i => !i.Deleted).FirstOrDefault(i => i.Id == id);
        }

        public async Task CreateNew(AnimalManageViewModel model)
        {
            //var owner = await _accountsRepository.GetByIdFromUserManager(model.OwnerId);
            //var animalType = _animalTypesRepository.GetById(model.AnimalTypeId);

            Animal animal = new Animal()
            {
                //Owner = owner,
                //AnimalType = animalType,                
                Active = model.Active,
                AnimalDesc = model.Description,
                BirthDate = model.BirthDay,
                CreationDate = DateTime.Now,
                Deleted = false,
                Name = model.Name,
                AnimalTypeId = model.AnimalTypeId,
                OwnerId = model.OwnerId,                
            };

            var diseasesDbSelected = _diseasesRepository.Where(i => model.DiseaseIds.Contains(i.Id)).ToList();
            if (diseasesDbSelected.Any())
            {
                foreach (var disease in diseasesDbSelected)
                {
                    animal.AnimalDiseases.Add(new AnimalDisease { Disease = disease, Animal = animal});
                }
            }

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
            Animal animal = getByIdWithInclude(model.Id);
            if (animal != null)
            {
                animal.Active = model.Active;
                animal.ModificationDate = DateTime.Now;
                animal.Name = model.Name;
                animal.OwnerId = model.OwnerId;
                animal.AnimalDesc = model.Description;
                animal.AnimalTypeId = model.AnimalTypeId;
                animal.BirthDate = model.BirthDay;

                var diseasesDbSelected = _diseasesRepository.Where(i => model.DiseaseIds.Contains(i.Id)).ToList();
                var existingDiseases = animal.AnimalDiseases.Select(x => x.Disease).ToList();

                var diseasesToAddList = diseasesDbSelected.Except(existingDiseases).ToList();
                foreach (var disease in diseasesToAddList)
                {
                    animal.AnimalDiseases.Add(new AnimalDisease { Disease = disease, Animal = animal});
                }

                var diseasesToRemoveList = existingDiseases.Except(diseasesDbSelected).ToList();
                foreach (var disease in diseasesToRemoveList)
                {
                    animal.AnimalDiseases.Remove(animal.AnimalDiseases.First(x => x.Disease == disease));
                }

                await base.SaveChangesAsync();
            }
        }

        public AnimalManageViewModel GetCreateNewViewModel()
        {
            AnimalManageViewModel model = new AnimalManageViewModel();

            model.OwnersSelectList = _accountsRepository.GetUsersSelectList();
            model.AnimalTypesSelectList = _animalTypesRepository.GetAnimalTypesSelectList();
            model.DiseasesSelectList = _diseasesRepository.GetDiseasesSelectList();

            return model;
        }

        public AnimalManageViewModel GetEditViewModel(int id)
        {
            AnimalManageViewModel model = null;

            Animal animal = getByIdWithInclude(id);
            if (animal != null)
            {
                model = new AnimalManageViewModel();

                model.Active = animal.Active;
                model.Name = animal.Name;
                model.OwnerId = animal.Owner.Id;
                model.OwnersSelectList = _accountsRepository.GetUsersSelectList();
                model.Description = animal.AnimalDesc;
                model.AnimalTypeId = animal.AnimalType.Id;
                model.AnimalTypesSelectList = _animalTypesRepository.GetAnimalTypesSelectList();
                model.DiseaseIds = animal.AnimalDiseases.Select(i => i.DiseaseId).ToList();
                model.DiseasesSelectList = _diseasesRepository.GetDiseasesSelectList();
                model.BirthDay = animal.BirthDate;
            }

            return model;
        }

        public AnimalManageViewModel GetAllSelectListProperties(AnimalManageViewModel model)
        {
            if(model != null)
            {
                model.DiseasesSelectList = _diseasesRepository.GetDiseasesSelectList();
                model.AnimalTypesSelectList = _animalTypesRepository.GetAnimalTypesSelectList();
                model.OwnersSelectList = _accountsRepository.GetUsersSelectList();
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

        public IEnumerable<SelectListItem> GetAnimalsSelectList()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            list = base.GetAllActive().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }).ToList();

            return list.AsEnumerable();
        }

        public IEnumerable<SelectListItem> GetUserAnimalsSelectList(string userId)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            if (!string.IsNullOrEmpty(userId))
            {
                list = base.GetAllActive().Where(i => i.Owner.Id == userId).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }).ToList();
            }
            return list.AsEnumerable();
        }

        private Animal getByIdWithInclude(int id)
        {
            return _db.Animals.Include(x=>x.AnimalType).Include(x=>x.Owner).Include(x => x.Visits).Include(x => x.AnimalMedicalExaminations).Include(x => x.AnimalDiseases).Where(i => i.Active && !i.Deleted && i.Id == id).FirstOrDefault();
        }

        public async Task<Animal> InsertFromVisitFormAsync(VisitMakeVisitViewModel model)
        {
            if(model.AnimalId != null)
            {
                return GetById(model.AnimalId);
            }

            var owner = await _accountsRepository.GetByIdFromUserManager(model.UserId);
            var animalType = _animalTypesRepository.GetById(model.AnimalTypeId);

            Animal animal = new Animal()
            {
                Active = true,
                AnimalType = animalType,
                BirthDate = model.AnimalBirthdate,
                Owner = owner,
                Name = model.AnimalName,
                CreationDate = DateTime.Now,
            };

            await base.InsertAsync(animal);

            return animal;
        }
    }
}
