using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Weterynarz.Domain.EntitiesDb
{
    public class AnimalDisease
    {
        public int AnimalId { get; set; }
        [ForeignKey("AnimalId")]
        public virtual Animal Animal { get; set; }

        public int DiseaseId { get; set; }
        [ForeignKey("DiseaseId")]
        public virtual Disease Disease { get; set; }
    }
}
