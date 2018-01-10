using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Weterynarz.Domain.ContextDb;
using Weterynarz.Domain.EntitiesDb;
using Weterynarz.Domain.Repositories.Interfaces;
using System.Linq;

namespace Weterynarz.Domain.Repositories.Implementations
{
    public class AnimalTypesRepository : BaseRepository<AnimalType>, IAnimalTypesRepository
    {
        public AnimalTypesRepository(ApplicationDbContext db) : base(db)
        {
        }

        public IEnumerable<SelectListItem> GetAnimalTypesSelectList()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            list = base.GetAllActive().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }).ToList();

            list.Insert(0, new SelectListItem { Value = "", Text = "-- wybierz --", Disabled = true, Selected = true });

            return list.AsEnumerable();
        }
    }
}
