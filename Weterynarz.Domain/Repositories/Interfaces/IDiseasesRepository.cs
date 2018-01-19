using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weterynarz.Domain.EntitiesDb;
using Weterynarz.Domain.ViewModels.Disease;

namespace Weterynarz.Domain.Repositories.Interfaces
{
    public interface IDiseasesRepository : IBaseRepository<Disease>
    {
        IEnumerable<SelectListItem> GetDiseasesSelectList();
        IQueryable<DiseaseIndexViewModel> GetIndexViewModel();
        DiseaseManageViewModel GetEditViewModel(int id);
        Task CreateNew(DiseaseManageViewModel model);
        Task Edit(DiseaseManageViewModel model);
        Task Delete(int id);
    }
}
