using System;
using System.Collections.Generic;
using System.Text;
using Weterynarz.Basic.Enum;

namespace Weterynarz.Domain.ViewModels.PriceList
{
    public class PriceListIndexViewModel : BaseViewModel<int>
    {
        public string Name { get; set; }
        public PriceListEntryType Type { get; set; }
        public string Price { get; set; }
    }
}
