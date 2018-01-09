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
        private IAnimalRepository _animalRepository;

        public VisitService(IAccountsService accountsService, IAnimalRepository animalRepository)
        {
            _accountsService = accountsService;
            _animalRepository = animalRepository;
        }

        public async Task<VisitMakeVisitViewModel> GetMakeVisitViewModel(string userId)
        {
            VisitMakeVisitViewModel model = new VisitMakeVisitViewModel();

            if(!string.IsNullOrEmpty(userId))
            {
                model.AnimalsSelectList = _animalRepository.GetUserAnimalsSelectList(userId);
            }
            model.VetsSelectList = await _accountsService.GetVetsSelectList();           

            return model;
        }
    }
}
