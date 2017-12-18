using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weterynarz.Services.ViewModels.MedicalExaminationTypes;

namespace Weterynarz.Services.Services.Interfaces
{
    public interface IMedicalExaminationTypesService
    {
        IQueryable<MedicalExaminationTypesIndexViewModel> GetIndexViewModel();
        MedicalExaminationTypesManageViewModel GetEditViewModel(int id);
        Task CreateNewType(MedicalExaminationTypesManageViewModel model);
        Task<bool> EditType(MedicalExaminationTypesManageViewModel model);
        Task<bool> DeleteType(int id);
    }
}
