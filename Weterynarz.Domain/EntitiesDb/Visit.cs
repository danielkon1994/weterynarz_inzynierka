using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Weterynarz.Domain.EntitiesDb
{
    public class Visit : BaseEntity
    {
        [Required]
        public DateTime VisitDate { get; set; }

        public string DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public virtual ApplicationUser Doctor { get; set; }

        public string ClientId { get; set; }
        [ForeignKey("ClientId")]
        public virtual ApplicationUser Client { get; set; }

        [Required]
        public int AnimalId { get; set; }
        [ForeignKey("AnimalId")]
        public virtual Animal Animal { get; set; }

        [Required]
        public string AnimalDescription { get; set; }

        // zatwierdzona wizyta
        public bool Approved { get; set; } = false;
    }
}
