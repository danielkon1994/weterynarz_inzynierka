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
            _animalTypesRepository = animalTypesRepository;
            _accountsRepository = accountsRepository;
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

        public async Task<VisitMakeVisitViewModel> GetMakeVisitViewModel(string userId)
        {
            VisitMakeVisitViewModel model = new VisitMakeVisitViewModel();

            if (!string.IsNullOrEmpty(userId))
            {
                var user = _accountsRepository.GetById(userId);
                if (user != null)
                {
                    model.AnimalsSelectList = _animalRepository.GetUserAnimalsSelectList(userId);
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

        public async Task InsertNewVisit(VisitMakeVisitViewModel model)
        {
            Visit visitEntity = new Visit();
            ApplicationUser client;
            
            // Create new client if userId is empty
            if (string.IsNullOrEmpty(model.UserId))
            {
                client = new ApplicationUser()
                {
                    Active = true,
                    Address = model.Address,
                    City = model.City,
                    CreationDate = DateTime.Now,
                    Deleted = false,
                    Email = model.Email,
                    EmailConfirmed = true,
                    HouseNumber = model.HomeNumber,
                    Name = model.Name,
                    Surname = model.Surname,
                    ZipCode = model.ZipCode,
                    UserName = model.UserName
                };

                client.PasswordHash = Crypto.HashPassword(model.Password);

                await _accountsRepository.InsertAcync(client);

                model.UserId = client.Id;
            }

            //
            visitEntity = new Visit()
            {

            };
        }
    }
}
