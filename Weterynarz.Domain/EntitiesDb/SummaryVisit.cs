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
        [Required]
        public int VisitId { get; set; }
        [ForeignKey("VisitId")]
        public virtual Visit Visit { get; set; }

        public string Drugs { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
