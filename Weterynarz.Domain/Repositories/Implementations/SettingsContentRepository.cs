using System;
using System.Collections.Generic;
using System.Text;
using Weterynarz.Domain.ContextDb;
using Weterynarz.Domain.EntitiesDb;
using Weterynarz.Domain.Repositories.Interfaces;

namespace Weterynarz.Domain.Repositories.Implementations
{
    public class SettingsContentRepository : BaseRepository<SettingsContent>, ISettingsContentRepository
    {
        public SettingsContentRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
