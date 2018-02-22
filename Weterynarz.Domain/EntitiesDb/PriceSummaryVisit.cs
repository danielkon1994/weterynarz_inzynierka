using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Weterynarz.Domain.EntitiesDb
{
    public class PriceSummaryVisit
    {
        public int PriceListId { get; set; }
        [ForeignKey("PriceListId")]
        public virtual PriceList PriceList { get; set; }

        public int SummaryVisitId { get; set; }
        [ForeignKey("SummaryVisitId")]
        public virtual SummaryVisit SummaryVisit { get; set; }
    }
}
