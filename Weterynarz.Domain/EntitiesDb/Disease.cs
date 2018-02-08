using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Weterynarz.Domain.EntitiesDb
{
    public class Disease : BaseEntity
    {
        public Disease()
        {
            AnimalDiseases = new Collection<AnimalDisease>();
        }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<AnimalDisease> AnimalDiseases { get; set; }
    }
}
