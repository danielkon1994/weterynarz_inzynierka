using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Weterynarz.Domain.EntitiesDb
{
    public class DoctorGraphic : BaseEntity
    {
        [Required]
        public DateTime AvailableFrom { get; set; }

        [Required]
        public DateTime AvailableTo { get; set; }

        [Required]
        public string DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public virtual ApplicationUser Doctor { get; set; }

        [Required]
        public virtual Graphic Graphic { get; set; }
    }
}
