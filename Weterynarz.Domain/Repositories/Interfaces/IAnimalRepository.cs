using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using Weterynarz.Domain.EntitiesDb;

namespace Weterynarz.Domain.Repositories.Interfaces
{
    public interface IAnimalRepository : IBaseRepository<Animal>
    {
        IEnumerable<SelectListItem> GetUserAnimalsSelectList(string userId);
    }
}
