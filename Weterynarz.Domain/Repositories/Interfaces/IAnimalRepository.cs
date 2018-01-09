using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Weterynarz.Domain.Repositories.Interfaces
{
    public interface IAnimalRepository
    {
        IEnumerable<SelectListItem> GetUserAnimalsSelectList(string userId);
    }
}
