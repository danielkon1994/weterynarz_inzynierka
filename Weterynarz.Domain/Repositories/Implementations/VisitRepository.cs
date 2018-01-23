using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Weterynarz.Domain.ContextDb;
using Weterynarz.Domain.EntitiesDb;
using Weterynarz.Domain.Repositories.Interfaces;
using System.Linq;
using System.Data.Entity;
using Weterynarz.Domain.ViewModels.Visit;
using CryptoHelper;

namespace Weterynarz.Domain.Repositories.Implementations
{
    public class VisitRepository : BaseRepository<Visit>, IVisitRepository
    {
        private IUsersRepository _userRepository;
        private IAccountsRepository _accountsRepository;
        private IAnimalRepository _animalRepository;
        private IAnimalTypesRepository _animalTypesRepository;

        public VisitRepository(ApplicationDbContext db, IAnimalRepository animalRepository,
            IUsersRepository userRepository,
            IAccountsRepository accountsRepository,
            IAnimalTypesRepository animalTypesRepository) : base(db)
        {
            _animalRepository = animalRepository;
            _userRepository = userRepository;
            _accountsRepository = accountsRepository;
            _animalTypesRepository = animalTypesRepository;
        }

        public bool CheckVisitExists(DateTime visitDate)
        {
            var visitExist = base.GetAllActive().Any();
            if(visitExist)
            {
                return true;
            }

            return false;
        }

        public async Task CreateNew(VisitManageViewModel model)
        {
            Visit animal = new Visit()
            {
                Active = model.Active,
                CreationDate = DateTime.Now,
                Deleted = false,
                AnimalDescription = model.Description,
                AnimalId = model.AnimalId,
                DoctorId = model.DoctorId,
                VisitDate = model.VisitDate                
            };

            await base.InsertAsync(animal);
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Edit(VisitManageViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<VisitManageViewModel> GetCreateNewViewModel()
        {
            VisitManageViewModel model = new VisitManageViewModel();
            model.DoctorsSelectList = await _accountsRepository.GetVetsSelectList();
            model.AnimalsSelectList = _animalTypesRepository.GetAnimalTypesSelectList();

            return model;
        }

        public async Task<VisitManageViewModel> GetEditViewModel(int id)
        {
            VisitManageViewModel model = null;

            Visit visit = base.GetById(id);
            if (visit != null)
            {
                model = new VisitManageViewModel();

                model.Active = visit.Active;
                model.DoctorId = visit.DoctorId;
                model.DoctorsSelectList = await _accountsRepository.GetVetsSelectList();
                model.Description = visit.AnimalDescription;
                model.AnimalId = visit.AnimalId;
                model.AnimalsSelectList = _animalRepository.GetUserAnimalsSelectList(visit.OwnerId);
                model.VisitDate = visit.VisitDate;
            }

            return model;
        }

        public IQueryable<VisitIndexViewModel> GetIndexViewModel()
        {
            return base.GetAllNotDeleted().Select(i => new VisitIndexViewModel
            {
                Id = i.Id,
                Active = i.Active,
                VisitDate = i.VisitDate,
                CreationDate = i.CreationDate,
                Owner = i.Animal.Owner.Name + " " + i.Animal.Owner.Surname,
                Animal = i.Animal.Name + " (" + i.Animal.AnimalType.Name + ")",
                Doctor = i.Doctor.Name + " " + i.Doctor.Surname,
                Approved = i.Approved
            });
        }

        public async Task<VisitMakeVisitViewModel> GetMakeVisitViewModel(ApplicationUser user)
        {
            VisitMakeVisitViewModel model = new VisitMakeVisitViewModel();

            if(user != null)
            { 
                if (!string.IsNullOrEmpty(user.Id))
                {
                    model.AnimalsSelectList = _animalRepository.GetUserAnimalsSelectList(user.Id);
                    model.UserName = user.UserName;
                    model.Email = user.Email;
                    model.Name = user.Name;
                    model.Surname = user.Surname;
                    model.HomeNumber = user.HouseNumber;
                    model.City = user.City;
                    model.ZipCode = user.ZipCode;
                    model.Address = user.Address;
                    model.UserId = user.Id;
                }
            }
            model.VetsSelectList = await _accountsRepository.GetVetsSelectList();
            model.AnimalTypesSelectList = _animalTypesRepository.GetAnimalTypesSelectList();

            return model;
        }

        public async Task<VisitMakeVisitViewModel> GetMakeVisitViewModel(VisitMakeVisitViewModel model)
        {
            if (!string.IsNullOrEmpty(model.UserId))
            {
                var user = _accountsRepository.GetById(model.UserId);
                if (user != null)
                {
                    model.AnimalsSelectList = _animalRepository.GetUserAnimalsSelectList(model.UserId);
                }
            }
            model.VetsSelectList = await _accountsRepository.GetVetsSelectList();
            model.AnimalTypesSelectList = _animalTypesRepository.GetAnimalTypesSelectList();

            return model;
        }

        public async Task InsertFromVisitFormAsync(VisitMakeVisitViewModel model)
        {
            // Create new client if userId is empty
            string userId = string.IsNullOrEmpty(model.UserId) ? await _accountsRepository.InsertFromVisitFormAsync(model) : model.UserId;

            int animalId = model.AnimalId ?? await _animalRepository.InsertFromVisitFormAsync(model);

            // create new visit
            Visit visit = new Visit()
            {
                Active = true,
                AnimalId = animalId,
                Approved = false,
                CreationDate = DateTime.Now,
                DoctorId = model.VetId,
                VisitDate = model.VisitDate,                
            };

            await base.InsertAsync(visit);
        }
    }
}
