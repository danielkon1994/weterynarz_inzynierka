using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Weterynarz.Domain.ContextDb;
using Weterynarz.Domain.EntitiesDb;
using Weterynarz.Domain.Repositories.Interfaces;

namespace Weterynarz.Domain.Repositories.Implementations
{
    public class MedicalExaminationTypesRepository : BaseRepository<MedicalExaminationType>, IMedicalExaminationTypesRepository
    {
        public MedicalExaminationTypesRepository(ApplicationDbContext db) : base(db)
        {
        }

        
    }
}
