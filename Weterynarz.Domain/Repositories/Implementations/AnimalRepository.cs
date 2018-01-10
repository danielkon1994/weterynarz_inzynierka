using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Weterynarz.Domain.ContextDb;
using Weterynarz.Domain.EntitiesDb;
using Weterynarz.Domain.Repositories.Interfaces;
using System.Linq;

namespace Weterynarz.Domain.Repositories.Implementations
{
    public class AnimalRepository : BaseRepository<Animal>, IAnimalRepository
    {
        private ApplicationDbContext _db;

        public AnimalRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetUserAnimalsSelectList(string userId)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "", Text = "-- wybierz --", Disabled = true, Selected = true });

            if(!string.IsNullOrEmpty(userId))
            {
                list = base.GetAllActive().Where(i => i.Owner.Id == userId).Select(i => new SelectListItem {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }).ToList();
            }
            return list.AsEnumerable();
        }
    }
}
