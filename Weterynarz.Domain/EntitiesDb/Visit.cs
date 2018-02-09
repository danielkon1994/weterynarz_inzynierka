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
        public string DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public virtual ApplicationUser Doctor { get; set; }

        [Required]
        public int AnimalId { get; set; }
        [ForeignKey("AnimalId")]
        public virtual Animal Animal { get; set; }
        
        public virtual SummaryVisit SummaryVisit { get; set; }
        
        public string ReasonVisit { get; set; }

        public bool Approved { get; set; } = false;

        public string Description { get; set; }
    }
}
