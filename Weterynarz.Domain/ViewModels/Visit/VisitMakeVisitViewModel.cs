﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Weterynarz.Basic.Const;

namespace Weterynarz.Domain.ViewModels.Visit
{
    public class VisitMakeVisitViewModel
    {
        #region Contact details    
        [Required]
        public string UserName { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Hasła muszą być takie same")]
        public string ComparePassword { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        public string Address { get; set; }

        public string HomeNumber { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }
        #endregion

        public string UserId { get; set; }

        public IEnumerable<SelectListItem> VetsSelectList { get; set; }
        public string VetId { get; set; }

        public IEnumerable<SelectListItem> AnimalsSelectList { get; set; }
        public int? AnimalId { get; set; }

        public IEnumerable<SelectListItem> AnimalTypesSelectList { get; set; }
        public int AnimalTypeId { get; set; }

        public string AnimalName { get; set; }

        [DataType(DataType.Date)]
        public DateTime AnimalBirthdate { get; set; }

        public string DescriptionProblem { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime VisitDate { get; set; }
    }
}