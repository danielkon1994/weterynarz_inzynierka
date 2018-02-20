using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Weterynarz.Domain.EntitiesDb
{
    public class MedicalExaminationType : BaseEntity
    {
        public MedicalExaminationType()
        {
            AnimalMedicalExaminations = new Collection<AnimalMedicalExamination>();
        }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<AnimalMedicalExamination> AnimalMedicalExaminations { get; set; }

        public virtual PriceList Price { get; set; }
    }
}
