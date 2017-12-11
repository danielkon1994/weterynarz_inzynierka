﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Weterynarz.Domain.EntitiesDb
{
    public class MedicalExaminationType : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public DateTime Description { get; set; }
    }
}