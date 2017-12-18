using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weterynarz.Domain.EntitiesDb;
using Weterynarz.Domain.Repositories.Interfaces;
using Weterynarz.Services.Services.Interfaces;
using Weterynarz.Services.ViewModels.AnimalTypes;
using Weterynarz.Services.ViewModels.MedicalExaminationTypes;

namespace Weterynarz.Services.Services.Implementations
{
    public class MedicalExaminationTypesService : IMedicalExaminationTypesService
    {
        public IMedicalExaminationTypesRepository _medicalExaminationTypesRepository;

        public MedicalExaminationTypesService(IMedicalExaminationTypesRepository medicalExaminationTypesRepository)
        {
            this._medicalExaminationTypesRepository = medicalExaminationTypesRepository;
        }

        public IQueryable<MedicalExaminationTypesIndexViewModel> GetIndexViewModel()
        {
            return _medicalExaminationTypesRepository.GetAllNotDeleted().Select(a => new MedicalExaminationTypesIndexViewModel
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

            await _medicalExaminationTypesRepository.InsertAsync(type);
        }

        public MedicalExaminationTypesManageViewModel GetEditViewModel(int id)
        {
            MedicalExaminationTypesManageViewModel model;
            var medicalExaminationType = _medicalExaminationTypesRepository.GetById(id);
            if (medicalExaminationType != null)
            {
                model = new MedicalExaminationTypesManageViewModel {
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
            var medicalExaminationType = _medicalExaminationTypesRepository.GetById(model.Id);
            if (medicalExaminationType != null)
            {
                medicalExaminationType.Active = model.Active;
                medicalExaminationType.Description = model.Description;
                medicalExaminationType.Name = model.Name;
                medicalExaminationType.ModificationDate = DateTime.Now;

                await _medicalExaminationTypesRepository.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteType(int id)
        {
            var medicalExaminationType = _medicalExaminationTypesRepository.GetById(id);
            if(medicalExaminationType != null)
            { 
                try
                {
                    await _medicalExaminationTypesRepository.SoftDeleteAsync(medicalExaminationType);
                    return true;
                }
                catch(Exception ex)
                {
                    return false;
                }                
            }

            return false;
        }
    }
}
