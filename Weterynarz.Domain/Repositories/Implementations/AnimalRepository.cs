using System;
using System.Collections.Generic;
using System.Text;
using Weterynarz.Domain.ContextDb;
using Weterynarz.Domain.EntitiesDb;
using Weterynarz.Domain.Repositories.Interfaces;

namespace Weterynarz.Domain.Repositories.Implementations
{
    public class AnimalRepository : BaseRepository<Animal>, IAnimalRepository
    {
        private ApplicationDbContext _db;

        public AnimalRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

    }
}
