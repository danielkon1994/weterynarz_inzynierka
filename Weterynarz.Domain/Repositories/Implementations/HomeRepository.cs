using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Weterynarz.Basic.Const;
using Weterynarz.Domain.Repositories.Interfaces;
using Weterynarz.Domain.Services;
using Weterynarz.Domain.ViewModels.Doctor;
using Weterynarz.Domain.ViewModels.Home;

namespace Weterynarz.Domain.Repositories.Implementations
{
    public class HomeRepository : IHomeRepository
    {
        private IMemoryCacheService _memoryCacheService;
        private IDoctorGraphicsRepository _doctorGraphicsRepository;
        private IUsersRepository _usersRepository;

        public HomeRepository(IMemoryCacheService memoryCacheService,
            IDoctorGraphicsRepository doctorGraphicsRepository,
            IUsersRepository usersRepository)
        {
            _memoryCacheService = memoryCacheService;
            _doctorGraphicsRepository = doctorGraphicsRepository;
            _usersRepository = usersRepository;
        }

        public HomeIndexViewModel GetIndexViewModel()
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            Dictionary<string, string> siteContent = _memoryCacheService.GetContentToDict(CacheKey.SettingsContentCache);
            model.SiteContent = siteContent;
            
            var availableDoctorsList = _usersRepository.GetListUsersViewModel().Where(u => u.Roles.Contains<string>(UserRoles.Doctor)).ToList();
            foreach(var doctor in availableDoctorsList)
            {
                DoctorShowGraphicViewModel doctorGraphic = _doctorGraphicsRepository.GetDoctorGraphicToShowViewModel(doctor.Id);
                model.Doctors.Add(new ViewModels.Doctor.DoctorHomeItem() { Name = doctor.Name, Surname = doctor.Surname, Specialization = doctor.Specialization, Graphic = doctorGraphic });           
            }

            return model;
        }
    }
}
