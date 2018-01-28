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

        public async Task Approved(Visit visit)
        {
            visit.Approved = true;

            await base.SaveChangesAsync();
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
            Visit visit = new Visit()
            {
                Active = true,
                CreationDate = DateTime.Now,
                Deleted = false,
                ReasonVisit = model.ReasonVisit,
                AnimalId = model.AnimalId,
                DoctorId = model.DoctorId,
                OwnerId = model.OwnerId,
                Approved = model.Approved,
                VisitDate = model.VisitDate,         
            };

            await base.InsertAsync(visit);
        }

        public async Task Delete(int id)
        {
            Visit visit = base.GetById(id);
            if (visit != null)
            {
                visit.Active = false;
                visit.Deleted = true;
            }

            await base.SaveChangesAsync();
        }

        public async Task Edit(VisitManageViewModel model)
        {
            Visit visit = base.GetById(model.Id);
            if (visit != null)
            {
                visit.Approved = model.Approved;
                visit.DoctorId = model.DoctorId;
                visit.ReasonVisit = model.ReasonVisit;
                visit.AnimalId = model.AnimalId;
                visit.VisitDate = model.VisitDate;
                visit.OwnerId = model.OwnerId;
            }

            await base.SaveChangesAsync();
        }

        public async Task<VisitManageViewModel> GetCreateNewViewModel(VisitManageViewModel model = null)
        {
            if(model == null)
                model = new VisitManageViewModel();

            model.DoctorsSelectList = await _accountsRepository.GetVetsSelectList();
            model.AnimalsSelectList = _animalTypesRepository.GetAnimalTypesSelectList();
            model.OwnersSelectList = await _accountsRepository.GetOwnersSelectList();

            return model;
        }

        public async Task<VisitManageViewModel> GetEditViewModel(int id)
        {
            VisitManageViewModel model = null;

            Visit visit = base.GetById(id);
            if (visit != null)
            {
                model = new VisitManageViewModel();
                model.Id = visit.Id;
                model.Approved = visit.Approved;
                model.DoctorId = visit.DoctorId;
                model.DoctorsSelectList = await _accountsRepository.GetVetsSelectList();
                model.ReasonVisit = visit.ReasonVisit;
                model.AnimalId = visit.AnimalId;
                model.AnimalsSelectList = _animalRepository.GetUserAnimalsSelectList(visit.OwnerId);
                model.VisitDate = visit.VisitDate;
                model.OwnerId = visit.OwnerId;
                model.OwnersSelectList = await _accountsRepository.GetOwnersSelectList();
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
