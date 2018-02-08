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
        private IMedicalExaminationTypesRepository _medicalExaminationRepository;
        private IDiseasesRepository _diseasesRepository;

        public VisitRepository(ApplicationDbContext db, IAnimalRepository animalRepository,
            IUsersRepository userRepository,
            IAccountsRepository accountsRepository,
            IAnimalTypesRepository animalTypesRepository,
            IMedicalExaminationTypesRepository medicalExaminationRepository,
            IDiseasesRepository diseasesRepository) : base(db)
        {
            _animalRepository = animalRepository;
            _userRepository = userRepository;
            _accountsRepository = accountsRepository;
            _animalTypesRepository = animalTypesRepository;
            _medicalExaminationRepository = medicalExaminationRepository;
            _diseasesRepository = diseasesRepository;
        }

        public async Task Approved(Visit visit)
        {
            visit.Approved = true;

            await base.SaveChangesAsync();
        }

        public bool CheckVisitExists(DateTime visitDate)
        {
            DateTime visitDateRange = visitDate.AddMinutes(30);
            bool visitExist = base.GetAllActive().Any(v => v.VisitDate == visitDate || (v.VisitDate >= visitDate && v.VisitDate <= visitDateRange));
            if(visitExist)
            {
                return true;
            }

            return false;
        }

        public async Task CreateNew(VisitManageViewModel model)
        {
            ApplicationUser doctor = await _accountsRepository.GetByIdFromUserManager(model.DoctorId);
            Animal animal = _animalRepository.GetById(model.AnimalId);

            Visit visit = new Visit()
            {
                Active = true,
                CreationDate = DateTime.Now,
                Deleted = false,
                ReasonVisit = model.ReasonVisit,
                Animal = animal,
                Doctor = doctor,
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
            ApplicationUser doctor = await _accountsRepository.GetByIdFromUserManager(model.DoctorId);
            Animal animal = _animalRepository.GetById(model.AnimalId);

            Visit visit = base.GetById(model.Id);
            if (visit != null)
            {
                visit.Approved = model.Approved;
                visit.Doctor = doctor;
                visit.ReasonVisit = model.ReasonVisit;
                visit.Animal = animal;
                visit.VisitDate = model.VisitDate;
                visit.ModificationDate = DateTime.Now;
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
                model.DoctorId = visit.Doctor.Id;
                model.DoctorsSelectList = await _accountsRepository.GetVetsSelectList();
                model.ReasonVisit = visit.ReasonVisit;
                model.AnimalId = visit.Animal.Id;
                model.AnimalsSelectList = _animalRepository.GetUserAnimalsSelectList(visit.Animal.Owner.Id);
                model.VisitDate = visit.VisitDate;
                model.OwnerId = visit.Animal.Owner.Id;
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
                Approved = i.Approved,
                SummaryId = i.SummaryVisit.Id,
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
            ApplicationUser doctor = await _accountsRepository.GetByIdFromUserManager(model.VetId);
            Animal animal = await _animalRepository.InsertFromVisitFormAsync(model);

            // create new visit
            Visit visit = new Visit()
            {
                Animal = animal,
                Description = model.ReasonVisit,
                Doctor = doctor,
                Active = true,
                Approved = false,
                CreationDate = DateTime.Now,
                VisitDate = model.VisitDate, 
                ReasonVisit = model.ReasonVisit
            };

            await base.InsertAsync(visit);
        }
    }
}
