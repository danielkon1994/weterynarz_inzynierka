﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weterynarz.Domain.EntitiesDb;
using Weterynarz.Domain.ViewModels.Visit;

namespace Weterynarz.Domain.Repositories.Interfaces
{
    public interface IVisitRepository : IBaseRepository<Visit>
    {
        bool CheckVisitExists(DateTime visitDate);
        Task<VisitMakeVisitViewModel> GetMakeVisitViewModel(ApplicationUser user);
        Task<VisitMakeVisitViewModel> GetMakeVisitViewModel(VisitMakeVisitViewModel model);
        Task InsertFromVisitFormAsync(VisitMakeVisitViewModel model);
        IQueryable<VisitIndexViewModel> GetIndexViewModel();
        VisitManageViewModel GetCreateNewViewModel();
        Task<VisitManageViewModel> GetEditViewModel(int id);
        Task CreateNew(VisitManageViewModel model);
        Task Edit(VisitManageViewModel model);
        Task Delete(int id);
    }
}
