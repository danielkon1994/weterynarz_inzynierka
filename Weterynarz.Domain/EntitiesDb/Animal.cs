using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Weterynarz.Domain.EntitiesDb
{
    public class Animal : BaseEntity
    {
        public Animal()
        {
            AnimalDiseases = new Collection<AnimalDisease>();
            Visits = new Collection<Visit>();
            AnimalMedicalExaminations = new Collection<AnimalMedicalExamination>();
        }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required]
        public string AnimalDesc { get; set; }

        [Required]
        public int AnimalTypeId { get; set; }
        [ForeignKey("AnimalTypeId")]
        public virtual AnimalType AnimalType { get; set; }

        [Required]
        public string OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public virtual ApplicationUser Owner { get; set; }

        public virtual ICollection<AnimalDisease> AnimalDiseases { get; set; }

        public virtual ICollection<AnimalMedicalExamination> AnimalMedicalExaminations { get; set; }

        public virtual ICollection<Visit> Visits { get; set; }
    }
}
