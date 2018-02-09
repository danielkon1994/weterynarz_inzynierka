using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Weterynarz.Domain.EntitiesDb
{
    public class AnimalMedicalExamination
    {
        public int AnimalId { get; set; }
        [ForeignKey("AnimalId")]
        public virtual Animal Animal { get; set; }

        public int MedicalExaminationId { get; set; }
        [ForeignKey("MedicalExaminationId")]
        public virtual MedicalExaminationType MedicalExamination { get; set; }
    }
}
