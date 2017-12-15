using System;
using System.Collections.Generic;
using System.Text;

namespace Weterynarz.Services.ViewModels.AnimalTypes
{
    public class AnimalTypesIndexViewModel : BaseViewModel<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
