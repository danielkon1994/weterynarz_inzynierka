using System;
using System.Collections.Generic;
using System.Text;
using Weterynarz.Basic.Const;
using Weterynarz.Domain.Repositories.Interfaces;
using Weterynarz.Domain.Services;
using Weterynarz.Domain.ViewModels.Home;

namespace Weterynarz.Domain.Repositories.Implementations
{
    public class HomeRepository : IHomeRepository
    {
        private IMemoryCacheService _memoryCacheService;

        public HomeRepository(IMemoryCacheService memoryCacheService)
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
