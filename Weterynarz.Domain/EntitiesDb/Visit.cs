using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Weterynarz.Domain.EntitiesDb
{
    public class Visit : BaseEntity
    {
        [Required]
        public DateTime VisitDate { get; set; }

        [Required]
        public ApplicationUser Doctor { get; set; }

        [Required]
        public Animal Animal { get; set; }
        
        public SummaryVisit SummaryVisit { get; set; }
        
        public string ReasonVisit { get; set; }

        public bool Approved { get; set; } = false;

        public string Description { get; set; }
    }
}
