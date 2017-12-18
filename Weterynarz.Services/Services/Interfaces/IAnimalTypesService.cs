using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weterynarz.Services.ViewModels.AnimalTypes;

namespace Weterynarz.Services.Services.Interfaces
{
    public interface IAnimalTypesService
    {
        IQueryable<AnimalTypesIndexViewModel> GetIndexViewModel();
        AnimalTypesManageViewModel GetEditViewModel(int id);
        Task CreateNewType(AnimalTypesManageViewModel model);
        Task<bool> EditType(AnimalTypesManageViewModel model);
        Task<bool> DeleteType(int id);
    }
}
