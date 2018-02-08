using System;
using System.Collections.Generic;
using System.Text;

namespace Weterynarz.Domain.EntitiesDb
{
    public class AnimalMedicalExamination
    {
        public int AnimalId { get; set; }
        public Animal Animal { get; set; }
        public int MedicalExaminationId { get; set; }
        public MedicalExaminationType MedicalExamination { get; set; }
    }
}
