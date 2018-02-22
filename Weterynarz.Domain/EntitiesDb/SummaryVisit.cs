using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Weterynarz.Domain.EntitiesDb
{
    public class SummaryVisit : BaseEntity
    {
        public SummaryVisit()
        {
            PriceSummaryVisit = new Collection<PriceSummaryVisit>();
        }

        [Required]
        public int VisitId { get; set; }
        [ForeignKey("VisitId")]
        public virtual Visit Visit { get; set; }

        public string Drugs { get; set; }

        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual ICollection<PriceSummaryVisit> PriceSummaryVisit { get; set; }
    }
}
