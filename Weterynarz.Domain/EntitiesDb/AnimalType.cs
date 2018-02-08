using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Weterynarz.Domain.EntitiesDb
{
    public class AnimalType : BaseEntity
    {
        public AnimalType()
        {
            Animals = new Collection<Animal>();
        }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Animal> Animals { get; set; }
    }
}
