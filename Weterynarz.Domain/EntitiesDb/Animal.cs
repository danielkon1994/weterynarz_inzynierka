using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Weterynarz.Domain.EntitiesDb
{
    public class Animal : BaseEntity
    {
        public Animal()
        {
            MedicalExaminations = new Collection<MedicalExamination>();
        }

        [Required]
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public string Description { get; set; }

        public virtual AnimalType AnimalType { get; set; }

        public virtual ApplicationUser Owner { get; set; }

        public virtual ICollection<MedicalExamination> MedicalExaminations { get; set; }
    }
}
