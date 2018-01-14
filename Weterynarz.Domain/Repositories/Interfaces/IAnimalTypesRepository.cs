using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weterynarz.Domain.EntitiesDb;
using Weterynarz.Domain.ViewModels.AnimalTypes;

namespace Weterynarz.Domain.Repositories.Interfaces
{
    public interface IAnimalTypesRepository : IBaseRepository<AnimalType>
    {
        IEnumerable<SelectListItem> GetAnimalTypesSelectList();
        IQueryable<AnimalTypesIndexViewModel> GetIndexViewModel();
        AnimalTypesManageViewModel GetEditViewModel(int id);
        Task CreateNewType(AnimalTypesManageViewModel model);
        Task<bool> EditType(AnimalTypesManageViewModel model);
        Task<bool> DeleteType(int id);
    }
}
