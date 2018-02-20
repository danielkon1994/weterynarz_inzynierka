using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weterynarz.Basic.Enum;
using Weterynarz.Domain.EntitiesDb;
using Weterynarz.Domain.ViewModels.Disease;
using Weterynarz.Domain.ViewModels.PriceList;

namespace Weterynarz.Domain.Repositories.Interfaces
{
    public interface IPriceListRepository : IBaseRepository<PriceList>
    {
        IEnumerable<SelectListItem> GetMedicalExaminationSelectList(PriceListEntryType type);
        IQueryable<PriceListIndexViewModel> GetIndexViewModel();
        PriceListManageViewModel GetEditViewModel(int id);
        Task CreateNew(PriceListManageViewModel model);
        Task Edit(PriceListManageViewModel model);
        Task Delete(int id);
    }
}
