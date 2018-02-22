using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Weterynarz.Basic.Enum;

namespace Weterynarz.Domain.EntitiesDb
{
    public class PriceList : BaseEntity
    {
        public PriceList()
        {
            PriceSummaryVisit = new Collection<PriceSummaryVisit>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public PriceListEntryType PriceListEntryType { get; set; }

        public int? MedicalExaminationId { get; set; }
        [ForeignKey("MedicalExaminationId")]
        public virtual MedicalExaminationType MedicalExamination { get; set; }

        public virtual ICollection<PriceSummaryVisit> PriceSummaryVisit { get; set; }
    }
}
