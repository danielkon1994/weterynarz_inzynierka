using System;
using System.Collections.Generic;
using System.Text;
using Weterynarz.Services.ViewModels.Home;

namespace Weterynarz.Services.Services.Interfaces
{
    public interface IHomeService
    {
        HomeIndexViewModel GetIndexViewModel();
    }
}
