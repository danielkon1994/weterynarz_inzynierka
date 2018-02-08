﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Weterynarz.Domain.EntitiesDb
{
    public class DoctorGraphic : BaseEntity
    {
        [Required]
        public DateTime AvailableFrom { get; set; }

        [Required]
        public DateTime AvailableTo { get; set; }

        [Required]
        public ApplicationUser Doctor { get; set; }

        [Required]
        public Graphic Graphic { get; set; }
    }
}
