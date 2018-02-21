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
using Weterynarz.Basic.Enum;
using Weterynarz.Domain.ViewModels.PriceList;
using Weterynarz.Domain.Extension;

namespace Weterynarz.Domain.Repositories.Implementations
{
    public class PriceListRepository : BaseRepository<PriceList>, IPriceListRepository
    {

        public PriceListRepository(ApplicationDbContext db) : base(db)
        {

        }

        public async Task CreateNew(PriceListManageViewModel model)
        {
            PriceList priceList = new PriceList()
            {
                Active = model.Active,
                CreationDate = DateTime.Now,
                Deleted = false,
                Name = model.Name,
                Price = model.Price,
                PriceListEntryType = model.Type
            };

            if(model.Type == PriceListEntryType.Examination && model.SelectedMedicalExaminationId != null)
            {
                priceList.MedicalExaminationId = model.SelectedMedicalExaminationId;
            }

            await base.InsertAsync(priceList);
        }

        public async Task Delete(int id)
        {
            PriceList priceList = base.GetById(id);
            if (priceList != null)
            {
                priceList.Deleted = true;

                await base.SaveChangesAsync();
            }
        }

        public async Task Edit(PriceListManageViewModel model)
        {
            PriceList priceList = base.GetById(model.Id);
            if (priceList != null)
            {
                priceList.Active = model.Active;
                priceList.ModificationDate = DateTime.Now;
                priceList.Name = model.Name;
                priceList.Price = model.Price;
                priceList.PriceListEntryType = model.Type;

                if (model.Type == PriceListEntryType.Examination && model.SelectedMedicalExaminationId != null)
                {
                    priceList.MedicalExaminationId = model.SelectedMedicalExaminationId;
                }

                await base.SaveChangesAsync();
            }
        }

        public PriceListManageViewModel GetEditViewModel(int id)
        {
            PriceListManageViewModel model = null;

            PriceList priceList = base.GetById(id);
            if (priceList != null)
            {
                model = new PriceListManageViewModel();

                model.Active = priceList.Active;
                model.Name = priceList.Name;
                model.Price = priceList.Price;
                model.Type = priceList.PriceListEntryType;
            }

            return model;
        }

        public IQueryable<PriceListIndexViewModel> GetIndexViewModel()
        {
            return base.GetAllNotDeleted().Select(i => new PriceListIndexViewModel
            {
                Id = i.Id,
                Active = i.Active,
                CreationDate = i.CreationDate,
                Name = i.Name,
                Price = i.Price,
                Type = i.PriceListEntryType
            });
        }

        public IEnumerable<SelectListItem> GetMedicalExaminationSelectList(PriceListEntryType type)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            list = base.GetAllActive().Where(i => i.MedicalExamination != null).Select(i => new SelectListItem
            {
                Text = i.MedicalExamination.Name,
                Value = i.MedicalExaminationId.ToString()
            }).ToList();

            return list.AsEnumerable();
        }

        public IEnumerable<ExtendSelectListItem> GetPriceListSelectList()
        {
            List<ExtendSelectListItem> list = new List<ExtendSelectListItem>();

            list = base.GetAllActive().Select(i => new ExtendSelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString(),
                DataAttribute = i.Price.ToString()
            }).ToList();

            return list.AsEnumerable();
        }
    }
}
