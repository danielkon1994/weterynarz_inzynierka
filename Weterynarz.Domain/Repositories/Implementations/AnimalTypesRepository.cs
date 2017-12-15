using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Weterynarz.Domain.ContextDb;
using Weterynarz.Domain.EntitiesDb;
using Weterynarz.Domain.Repositories.Interfaces;

namespace Weterynarz.Domain.Repositories.Implementations
{
    public class AnimalTypesRepository : BaseRepository<AnimalType>, IAnimalTypesRepository
    {
        public AnimalTypesRepository(ApplicationDbContext db) : base(db)
        {
        }

        
    }
}
