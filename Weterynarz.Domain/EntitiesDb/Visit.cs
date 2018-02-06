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
        public Visit()
        {
            Diseases = new Collection<Disease>();
            MedicalExaminations = new Collection<MedicalExaminationType>();
        }

        [Required]
        public DateTime VisitDate { get; set; }

        [Required]
        public string DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public virtual ApplicationUser Doctor { get; set; }

        [Required]
        public string OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public virtual ApplicationUser Owner { get; set; }

        [Required]
        public int AnimalId { get; set; }
        [ForeignKey("AnimalId")]
        public virtual Animal Animal { get; set; }

        public int SummaryVisitId { get; set; }
        [ForeignKey("SummaryVisitId")]
        public virtual SummaryVisit SummaryVisit { get; set; }
        
        public string ReasonVisit { get; set; }

        public bool Approved { get; set; } = false;

        public ICollection<Disease> Diseases { get; set; }

        public ICollection<MedicalExaminationType> MedicalExaminations { get; set; }

        public string Diagnosis { get; set; }
    }
}
