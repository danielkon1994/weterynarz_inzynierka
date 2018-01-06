using System;
using System.Collections.Generic;
using System.Text;
using Weterynarz.Basic.Const;
using Weterynarz.Services.Services.Interfaces;
using Weterynarz.Services.ViewModels.Home;

namespace Weterynarz.Services.Services.Implementations
{
    public class HomeService : IHomeService
    {
        private IMemoryCacheService _memoryCacheService;

        public HomeService(IMemoryCacheService memoryCacheService)
        {
            _memoryCacheService = memoryCacheService;
        }

        public HomeIndexViewModel GetIndexViewModel()
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            Dictionary<string, string> siteContent = _memoryCacheService.GetContentToDict(CacheKey.SettingsContentCache);
            model.SiteContent = siteContent;

            return model;
        }
    }
}
