using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Weterynarz.Services.ViewModels.Visit;

namespace Weterynarz.Services.Services.Interfaces
{
    public interface IVisitService
    {
        Task<VisitMakeVisitViewModel> GetMakeVisitViewModel(string userId);
    }
}
