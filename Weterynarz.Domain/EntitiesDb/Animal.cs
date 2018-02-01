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
            Diseases = new Collection<Disease>();
            Visits = new Collection<Visit>();
        }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        public string AnimalDesc { get; set; }

        public int AnimalTypeId { get; set; }
        [ForeignKey("AnimalTypeId")]
        public virtual AnimalType AnimalType { get; set; }

        public string OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public virtual ApplicationUser Owner { get; set; }

        public virtual ICollection<Disease> Diseases { get; set; }

        public virtual ICollection<Visit> Visits { get; set; }
    }
}
