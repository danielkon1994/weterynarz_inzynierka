using System;
using System.Collections.Generic;
using System.Text;

namespace Weterynarz.Domain.ViewModels.Animal
{
    public class AnimalIndexViewModel : BaseViewModel<int>
    {
        public string Name { get; set; }
        public DateTime BirthDay { get; set; }
        public string Owner { get; set; }
        public string Type { get; set; }        
    }
}
