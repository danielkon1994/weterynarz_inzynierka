using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weterynarz.Domain.ContextDb;
using Weterynarz.Domain.EntitiesDb;
using Weterynarz.Domain.Repositories.Interfaces;
using Weterynarz.Domain.ViewModels.MedicalExaminationTypes;

namespace Weterynarz.Domain.Repositories.Implementations
{
    public class MedicalExaminationTypesRepository : BaseRepository<MedicalExaminationType>, IMedicalExaminationTypesRepository
    {
        private ILogger<MedicalExaminationTypesRepository> _logger;

        public MedicalExaminationTypesRepository(ApplicationDbContext db, ILogger<MedicalExaminationTypesRepository> logger) : base(db)
        {
            _logger = logger;
        }

        public IQueryable<MedicalExaminationTypesIndexViewModel> GetIndexViewModel()
        {
            return base.GetAllNotDeleted().Select(a => new MedicalExaminationTypesIndexViewModel
            {
                Id = a.Id,
                Active = a.Active,
                Name = a.Name,
                Description = a.Description,
                CreationDate = a.CreationDate
            });
        }

        public async Task CreateNewType(MedicalExaminationTypesManageViewModel model)
        {
            MedicalExaminationType type = new MedicalExaminationType
            {
                Active = model.Active,
                CreationDate = DateTime.Now,
                Description = model.Description,
                Name = model.Name
            };

            await base.InsertAsync(type);
        }

        public MedicalExaminationTypesManageViewModel GetEditViewModel(int id)
        {
            MedicalExaminationTypesManageViewModel model;
            var medicalExaminationType = base.GetById(id);
            if (medicalExaminationType != null)
            {
                model = new MedicalExaminationTypesManageViewModel
                {
                    Active = medicalExaminationType.Active,
                    Name = medicalExaminationType.Name,
                    Id = medicalExaminationType.Id,
                    Description = medicalExaminationType.Description,
                };

                return model;
            }

            return null;
        }

        public async Task<bool> EditType(MedicalExaminationTypesManageViewModel model)
        {
            var medicalExaminationType = base.GetById(model.Id);
            if (medicalExaminationType != null)
            {
                medicalExaminationType.Active = model.Active;
                medicalExaminationType.Description = model.Description;
                medicalExaminationType.Name = model.Name;
                medicalExaminationType.ModificationDate = DateTime.Now;

                await base.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteType(int id)
        {
            var medicalExaminationType = base.GetById(id);
            if (medicalExaminationType != null)
            {
                try
                {
                    await base.SoftDeleteAsync(medicalExaminationType);
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Wystąpił błąd podczas usuwania typu badania");
                    return false;
                }
            }

            return false;
        }
    }
}
