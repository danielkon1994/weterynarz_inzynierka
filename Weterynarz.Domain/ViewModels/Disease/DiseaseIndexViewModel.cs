using System;
using System.Collections.Generic;
using System.Text;

namespace Weterynarz.Domain.ViewModels.Disease
{
    public class DiseaseIndexViewModel : BaseViewModel<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }        
    }
}
