using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Weterynarz.Domain.Repositories.Interfaces;
using Weterynarz.Services.Services.Interfaces;
using Weterynarz.Services.ViewModels.Visit;

namespace Weterynarz.Services.Services.Implementations
{
    public class VisitService : IVisitService
    {
        private IAccountsService _accountsService;
        private IAccountsRepository _accountsRepository;
        private IAnimalRepository _animalRepository;
        private IAnimalTypesRepository _animalTypesRepository;

        public VisitService(
            IAccountsService accountsService, 
            IAnimalRepository animalRepository, 
            IAccountsRepository accountsRepository,
            IAnimalTypesRepository animalTypesRepository)
        {
            _accountsService = accountsService;
            _animalRepository = animalRepository;
            _accountsRepository = accountsRepository;
            _animalTypesRepository = animalTypesRepository;
        }

        public async Task<VisitMakeVisitViewModel> GetMakeVisitViewModel(string userId)
        {
            VisitMakeVisitViewModel model = new VisitMakeVisitViewModel();

            if(!string.IsNullOrEmpty(userId))
            {
                var user = _accountsRepository.GetById(userId);
                if(user != null)
                { 
                    model.AnimalsSelectList = _animalRepository.GetUserAnimalsSelectList(userId);
                    model.Name = user.Name;
                    model.Surname = user.Surname;
                    model.HomeNumber = user.HouseNumber;
                    model.City = user.City;
                    model.ZipCode = user.ZipCode;
                    model.Address = user.Address;
                }
            }
            model.VetsSelectList = await _accountsService.GetVetsSelectList();
            model.AnimalTypesSelectList = _animalTypesRepository.GetAnimalTypesSelectList();

            return model;
        }
    }
}
