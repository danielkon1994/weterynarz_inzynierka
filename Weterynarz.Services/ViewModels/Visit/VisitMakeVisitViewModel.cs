using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Weterynarz.Services.ViewModels.Visit
{
    public class VisitMakeVisitViewModel
    {
        #region Contact details        
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Address { get; set; }

        public string HomeNumber { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }
        #endregion

        public string UserId { get; set; }

        public IEnumerable<SelectListItem> VetsSelectList { get; set; }
        public string VetId { get; set; }

        public IEnumerable<SelectListItem> AnimalsSelectList { get; set; }
        public int AnimalId { get; set; }

        public DateTime VisitDate { get; set; }
    }
}
