using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Weterynarz.Domain.ContextDb;
using Weterynarz.Domain.EntitiesDb;
using Weterynarz.Domain.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using Weterynarz.Domain.ViewModels.Visit;
using Weterynarz.Domain.ViewModels.Animal;
using Microsoft.EntityFrameworkCore;
using Weterynarz.Domain.ViewModels.Disease;

namespace Weterynarz.Domain.Repositories.Implementations
{
    public class DiseasesRepository : BaseRepository<Disease>, IDiseasesRepository
    {

        public DiseasesRepository(ApplicationDbContext db) : base(db)
        {

        }

        public async Task CreateNew(DiseaseManageViewModel model)
        {
            Disease disease = new Disease()
            {
                Active = model.Active,
                Description = model.Description,
                CreationDate = DateTime.Now,
                Deleted = false,
                Name = model.Name,
            };

            await base.InsertAsync(disease);
        }

        public async Task Delete(int id)
        {
            Disease disease = base.GetById(id);
            if(disease != null)
            {
                disease.Deleted = true;

                await base.SaveChangesAsync();
            }
        }

        public async Task Edit(DiseaseManageViewModel model)
        {
            Disease disease = base.GetById(model.Id);
            if (disease != null)
            {
                disease.Active = model.Active;
                disease.ModificationDate = DateTime.Now;
                disease.Name = model.Name;
                disease.Description = model.Description;

                await base.SaveChangesAsync();
            }
        }

        public DiseaseManageViewModel GetEditViewModel(int id)
        {
            DiseaseManageViewModel model = null;

            Disease disease = base.GetById(id);
            if (disease != null)
            {
                model = new DiseaseManageViewModel();

                model.Active = disease.Active;
                model.Name = disease.Name;
                model.Description = disease.Description;
            }

            return model;
        }

        public IQueryable<DiseaseIndexViewModel> GetIndexViewModel()
        {
            return base.GetAllNotDeleted().Select(i => new DiseaseIndexViewModel
            {
                Id = i.Id,
                Active = i.Active,
                CreationDate = i.CreationDate,
                Name = i.Name,
                Description = i.Description
            });
        }

        public IEnumerable<SelectListItem> GetDiseasesSelectList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "", Text = "-- wybierz --", Disabled = true, Selected = true });

            list = base.GetAllActive().Select(i => new SelectListItem {
                Text = i.Name,
                Value = i.Id.ToString()
            }).ToList();

            return list.AsEnumerable();
        }
    }
}
