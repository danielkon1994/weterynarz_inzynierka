using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weterynarz.Domain.EntitiesDb;
using Weterynarz.Domain.ViewModels.Disease;
using Weterynarz.Domain.ViewModels.Doctor;

namespace Weterynarz.Domain.Repositories.Interfaces
{
    public interface IDoctorGraphicsRepository : IBaseRepository<DoctorGraphic>
    {
        IEnumerable<SelectListItem> GetDoctorGraphicsSelectList();
        IQueryable<DoctorGraphicItem> GetAllGraphicsForDoctorViewModel(string doctorId);
        DoctorShowGraphicViewModel GetDoctorGraphicToShowViewModel(string doctorId);
        Task CreateNew(DoctorGraphicManageViewModel model);
        Task<DoctorGraphicManageViewModel> GetEditGraphicViewModel(int id, string doctorId);
        Task Update(DoctorGraphicManageViewModel model);
        Task Delete(int id);
    }
}
