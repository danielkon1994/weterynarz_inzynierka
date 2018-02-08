using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weterynarz.Domain.EntitiesDb;
using Weterynarz.Domain.ViewModels.SummaryVisit;

namespace Weterynarz.Domain.Repositories.Interfaces
{
    public interface ISummaryVisitRepository : IBaseRepository<SummaryVisit>
    {
        IEnumerable<SelectListItem> GetSummaryVisitSelectList();
        SummaryVisitIndexViewModel GetIndexViewModel(int id);
        SummaryVisitManageViewModel GetEditViewModel(int id);
        SummaryVisitManageViewModel GetCreateViewModel(int visitId, SummaryVisitManageViewModel model = null);
        Task CreateNew(SummaryVisitManageViewModel model);
        Task<bool> Edit(SummaryVisitManageViewModel model);
        Task<bool> Delete(int id);
    }
}
