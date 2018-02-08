﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weterynarz.Domain.EntitiesDb;
using Weterynarz.Domain.ViewModels.Animal;
using Weterynarz.Domain.ViewModels.Visit;

namespace Weterynarz.Domain.Repositories.Interfaces
{
    public interface IAnimalRepository : IBaseRepository<Animal>
    {
        IEnumerable<SelectListItem> GetUserAnimalsSelectList(string userId);
        Task<Animal> InsertFromVisitFormAsync(VisitMakeVisitViewModel model);
        IQueryable<AnimalIndexViewModel> GetIndexViewModel();
        AnimalManageViewModel GetAllSelectListProperties(AnimalManageViewModel model);
        AnimalManageViewModel GetCreateNewViewModel();
        AnimalManageViewModel GetEditViewModel(int id);
        Task CreateNew(AnimalManageViewModel model);
        Task Edit(AnimalManageViewModel model);
        Task Delete(int id);
    }
}
