using System;
using System.Collections.Generic;
using System.Text;
using Weterynarz.Domain.Repositories.Interfaces;
using Weterynarz.Services.Services.Interfaces;

namespace Weterynarz.Services.Services.Implementations
{
    public class AnimalService : IAnimalService
    {
        private IAnimalRepository _animalRepository { get; }

        public AnimalService(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }


    }
}
