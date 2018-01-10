using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using Weterynarz.Domain.EntitiesDb;

namespace Weterynarz.Domain.Repositories.Interfaces
{
    public interface IAnimalTypesRepository : IBaseRepository<AnimalType>
    {
        IEnumerable<SelectListItem> GetAnimalTypesSelectList();
    }
}
