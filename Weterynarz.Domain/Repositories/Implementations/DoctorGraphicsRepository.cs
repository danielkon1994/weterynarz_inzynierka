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
using Weterynarz.Domain.ViewModels.Doctor;
using Weterynarz.Basic.Const;

namespace Weterynarz.Domain.Repositories.Implementations
{
    public class DoctorGraphicsRepository : BaseRepository<DoctorGraphic>, IDoctorGraphicsRepository
    {

        public DoctorGraphicsRepository(ApplicationDbContext db) : base(db)
        {

        }

        public DoctorGraphic GetById(string id)
        {
            return _db.DoctorGraphics.Include("Graphic").Where(i => !i.Deleted).FirstOrDefault(i => i.DoctorId == id);
        }

        public override DoctorGraphic GetById(int id)
        {
            return _db.DoctorGraphics.Include("Graphic").Where(i => !i.Deleted).FirstOrDefault(i => i.Id == id);
        }

        public async Task CreateNew(DoctorGraphicManageViewModel model)
        {
            Graphic graphic = new Graphic()
            {
                Active = model.Active,
                CreationDate = DateTime.Now,
                MondayFrom = (int)model.MondayFrom.TotalMinutes,
                MondayTo = (int)model.MondayTo.TotalMinutes,
                TuesdayFrom = (int)model.TuesdayFrom.TotalMinutes,
                TuesdayTo = (int)model.TuesdayTo.TotalMinutes,
                WednesdayFrom = (int)model.WednesdayFrom.TotalMinutes,
                WednesdayTo = (int)model.WednesdayTo.TotalMinutes,
                ThursdayFrom = (int)model.ThursdayFrom.TotalMinutes,
                ThursdayTo = (int)model.ThursdayTo.TotalMinutes,
                FridayFrom = (int)model.FridayFrom.TotalMinutes,
                FridayTo = (int)model.FridayTo.TotalMinutes,
                SaturdayFrom = (int)model.SaturdayFrom.TotalMinutes,
                SaturdayTo = (int)model.SaturdayTo.TotalMinutes,
                SundayFrom = (int)model.SundayFrom.TotalMinutes,
                SundayTo = (int)model.SundayTo.TotalMinutes,
            };

            await _db.Graphics.AddAsync(graphic);

            DoctorGraphic doctorGraphic = new DoctorGraphic()
            {
                Graphic = graphic,
                CreationDate = DateTime.Now,
                Active = model.Active,
                DoctorId = model.DoctorId,
                AvailableFrom = model.AvailableFrom,
                AvailableTo = model.AvailableTo
            };

            await base.InsertAsync(doctorGraphic);
        }

        public async Task Update(DoctorGraphicManageViewModel model)
        {
            DoctorGraphic doctorGraphic = this.GetById(model.Id);
            if (doctorGraphic != null)
            {
                doctorGraphic.Graphic.Active = model.Active;
                doctorGraphic.Graphic.ModificationDate = DateTime.Now;
                doctorGraphic.Graphic.MondayFrom = (int)model.MondayFrom.TotalMinutes;
                doctorGraphic.Graphic.MondayTo = (int)model.MondayTo.TotalMinutes;
                doctorGraphic.Graphic.TuesdayFrom = (int)model.TuesdayFrom.TotalMinutes;
                doctorGraphic.Graphic.TuesdayTo = (int)model.TuesdayTo.TotalMinutes;
                doctorGraphic.Graphic.WednesdayFrom = (int)model.WednesdayFrom.TotalMinutes;
                doctorGraphic.Graphic.WednesdayTo = (int)model.WednesdayTo.TotalMinutes;
                doctorGraphic.Graphic.ThursdayFrom = (int)model.ThursdayFrom.TotalMinutes;
                doctorGraphic.Graphic.ThursdayTo = (int)model.ThursdayTo.TotalMinutes;
                doctorGraphic.Graphic.FridayFrom = (int)model.FridayFrom.TotalMinutes;
                doctorGraphic.Graphic.FridayTo = (int)model.FridayTo.TotalMinutes;
                doctorGraphic.Graphic.SaturdayFrom = (int)model.SaturdayFrom.TotalMinutes;
                doctorGraphic.Graphic.SaturdayTo = (int)model.SaturdayTo.TotalMinutes;
                doctorGraphic.Graphic.SundayFrom = (int)model.SundayFrom.TotalMinutes;
                doctorGraphic.Graphic.SundayTo = (int)model.SundayTo.TotalMinutes;


                doctorGraphic.ModificationDate = DateTime.Now;
                doctorGraphic.Active = model.Active;
                doctorGraphic.AvailableFrom = model.AvailableFrom;
                doctorGraphic.AvailableTo = model.AvailableTo;

                await base.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            DoctorGraphic doctorGraphic = base.GetById(id);
            if(doctorGraphic != null)
            {
                await base.SoftDeleteAsync(doctorGraphic);
            }
        }

        public async Task<DoctorGraphicManageViewModel> GetEditGraphicViewModel(int id, string doctorId)
        {
            DoctorGraphic doctorGraphic = GetById(doctorId);

            DoctorGraphicManageViewModel model = null;
            if (doctorGraphic != null)
            {
                model = new DoctorGraphicManageViewModel()
                {
                    Id = doctorGraphic.Id,
                    Active = doctorGraphic.Active,
                    AvailableFrom = doctorGraphic.AvailableFrom,
                    AvailableTo = doctorGraphic.AvailableTo,
                    DoctorId = doctorId,
                    MondayFrom = TimeSpan.FromMinutes(doctorGraphic.Graphic.MondayFrom),
                    MondayTo = TimeSpan.FromMinutes(doctorGraphic.Graphic.MondayTo),
                    TuesdayFrom = TimeSpan.FromMinutes(doctorGraphic.Graphic.TuesdayFrom),
                    TuesdayTo = TimeSpan.FromMinutes(doctorGraphic.Graphic.TuesdayTo),
                    WednesdayFrom = TimeSpan.FromMinutes(doctorGraphic.Graphic.WednesdayFrom),
                    WednesdayTo = TimeSpan.FromMinutes(doctorGraphic.Graphic.WednesdayTo),
                    ThursdayFrom = TimeSpan.FromMinutes(doctorGraphic.Graphic.ThursdayFrom),
                    ThursdayTo = TimeSpan.FromMinutes(doctorGraphic.Graphic.ThursdayTo),
                    FridayFrom = TimeSpan.FromMinutes(doctorGraphic.Graphic.FridayFrom),
                    FridayTo = TimeSpan.FromMinutes(doctorGraphic.Graphic.FridayTo),
                    SaturdayFrom = TimeSpan.FromMinutes(doctorGraphic.Graphic.SaturdayFrom),
                    SaturdayTo = TimeSpan.FromMinutes(doctorGraphic.Graphic.SaturdayTo),
                    SundayFrom = TimeSpan.FromMinutes(doctorGraphic.Graphic.SundayFrom),
                    SundayTo = TimeSpan.FromMinutes(doctorGraphic.Graphic.SundayTo),
                };
            }

            return model;
        }

        public IQueryable<DoctorGraphicItem> GetAllGraphicsForDoctorViewModel()
        {
            return base.GetAllNotDeleted().Select(i => new DoctorGraphicItem
            {
                Id = i.Id,
                Active = i.Active,
                CreationDate = i.CreationDate,
                AvailableFrom = i.AvailableFrom,
                AvailableTo = i.AvailableTo,
            });
        }

        public IEnumerable<SelectListItem> GetDoctorGraphicsSelectList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "", Text = "-- wybierz --", Disabled = true, Selected = true });

            list = base.GetAllActive().Select(i => new SelectListItem {
                Text = $"{i.AvailableFrom.ToString(FormatsDate.DateWithoutTime)} {i.AvailableTo.ToString(FormatsDate.DateWithoutTime)}",
                Value = i.Id.ToString()
            }).ToList();

            return list.AsEnumerable();
        }

        public DoctorShowGraphicViewModel GetDoctorGraphicToShowViewModel(string doctorId)
        {
            DoctorGraphic doctorGraphic = this.getActualGraphic(doctorId);

            DoctorShowGraphicViewModel model = null;
            if (doctorGraphic != null)
            {
                model = new DoctorShowGraphicViewModel()
                {
                    Id = doctorGraphic.Id,
                    MondayFrom = TimeSpan.FromMinutes(doctorGraphic.Graphic.MondayFrom),
                    MondayTo = TimeSpan.FromMinutes(doctorGraphic.Graphic.MondayTo),
                    TuesdayFrom = TimeSpan.FromMinutes(doctorGraphic.Graphic.TuesdayFrom),
                    TuesdayTo = TimeSpan.FromMinutes(doctorGraphic.Graphic.TuesdayTo),
                    WednesdayFrom = TimeSpan.FromMinutes(doctorGraphic.Graphic.WednesdayFrom),
                    WednesdayTo = TimeSpan.FromMinutes(doctorGraphic.Graphic.WednesdayTo),
                    ThursdayFrom = TimeSpan.FromMinutes(doctorGraphic.Graphic.ThursdayFrom),
                    ThursdayTo = TimeSpan.FromMinutes(doctorGraphic.Graphic.ThursdayTo),
                    FridayFrom = TimeSpan.FromMinutes(doctorGraphic.Graphic.FridayFrom),
                    FridayTo = TimeSpan.FromMinutes(doctorGraphic.Graphic.FridayTo),
                    SaturdayFrom = TimeSpan.FromMinutes(doctorGraphic.Graphic.SaturdayFrom),
                    SaturdayTo = TimeSpan.FromMinutes(doctorGraphic.Graphic.SaturdayTo),
                    SundayFrom = TimeSpan.FromMinutes(doctorGraphic.Graphic.SundayFrom),
                    SundayTo = TimeSpan.FromMinutes(doctorGraphic.Graphic.SundayTo),
                };
            }

            return model;
        }

        private DoctorGraphic getActualGraphic(string doctorId)
        {
            DateTime now = DateTime.Now;
            return _db.DoctorGraphics.Include("Graphic").Where(i => i.Active && !i.Deleted && (i.AvailableTo >= now && i.AvailableFrom <= now)).FirstOrDefault(i => i.DoctorId == doctorId);
        }
    }
}
