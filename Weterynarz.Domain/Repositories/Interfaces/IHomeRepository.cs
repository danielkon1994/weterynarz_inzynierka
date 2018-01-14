using System;
using System.Collections.Generic;
using System.Text;
using Weterynarz.Domain.ViewModels.Home;

namespace Weterynarz.Domain.Repositories.Interfaces
{
    public interface IHomeRepository
    {
        HomeIndexViewModel GetIndexViewModel();
    }
}
