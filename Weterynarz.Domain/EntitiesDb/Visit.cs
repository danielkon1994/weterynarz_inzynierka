using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Weterynarz.Domain.EntitiesDb
{
    public class Visit : BaseEntity
    {
        [Required]
        public DateTime VisitDate { get; set; }

        [Required]
        public virtual User Doctor { get; set; }

        [Required]
        public virtual Animal Animal { get; set; }

        public string Description { get; set; }
    }
}
