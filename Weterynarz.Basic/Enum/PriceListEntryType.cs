using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Weterynarz.Basic.Resources;

namespace Weterynarz.Basic.Enum
{
    public enum PriceListEntryType
    {
        [Display(ResourceType = typeof(ResAdmin), Name = "priceListEntryType_overall")]
        Overall = 1,
        [Display(ResourceType = typeof(ResAdmin), Name = "priceListEntryType_examination")]
        Examination = 2,
        [Display(ResourceType = typeof(ResAdmin), Name = "priceListEntryType_visit")]
        Visit = 3
    }
}
