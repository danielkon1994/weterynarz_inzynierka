﻿using System;
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
            return _animalTypesRepository.GetAllActive().Select(a => new AnimalTypesIndexViewModel
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
    }
}