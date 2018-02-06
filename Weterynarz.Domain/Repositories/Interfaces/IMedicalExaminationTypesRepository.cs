using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weterynarz.Domain.EntitiesDb;
using Weterynarz.Domain.ViewModels.MedicalExaminationTypes;

namespace Weterynarz.Domain.Repositories.Interfaces
{
    public interface IMedicalExaminationTypesRepository : IBaseRepository<MedicalExaminationType>
    {
        IQueryable<MedicalExaminationTypesIndexViewModel> GetIndexViewModel();
        MedicalExaminationTypesManageViewModel GetEditViewModel(int id);
        Task CreateNewType(MedicalExaminationTypesManageViewModel model);
        Task<bool> EditType(MedicalExaminationTypesManageViewModel model);
        Task<bool> DeleteType(int id);
        IEnumerable<SelectListItem> GetMedicalExaminationSelectList();
    }
}
