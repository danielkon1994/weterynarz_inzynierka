using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Weterynarz.Domain.EntitiesDb;
using Weterynarz.Domain.ViewModels.Visit;

namespace Weterynarz.Domain.Repositories.Interfaces
{
    public interface IAnimalRepository : IBaseRepository<Animal>
    {
        IEnumerable<SelectListItem> GetUserAnimalsSelectList(string userId);
        Task<int> InsertFromVisitFormAsync(VisitMakeVisitViewModel model);
    }
}
