using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Weterynarz.Domain.EntitiesDb
{
    public class MedicalExamination : BaseEntity
    {
        [Required]
        public DateTime ExaminationDate { get; set; }

        [Required]
        public int AnimalId { get; set; }
        [ForeignKey("AnimalId")]
        public virtual Animal Animal { get; set; }

        [Required]
        public virtual MedicalExaminationType MedicalExaminationType { get; set; }

        public string Description { get; set; }

        // Doctor Id
        public string DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public virtual ApplicationUser Doctor { get; set; }
    }
}
