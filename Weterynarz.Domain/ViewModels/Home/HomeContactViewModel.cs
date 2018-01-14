using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Weterynarz.Basic.Resources;

namespace Weterynarz.Domain.ViewModels.Home
{
    public class HomeContactViewModel
    {
        [Required(ErrorMessage = null, ErrorMessageResourceName = "homeContactRequiredErrorMessage", ErrorMessageResourceType = typeof(ResWebsite))]
        [Display(Name = "homeContactUserName", ResourceType = typeof(ResWebsite))]
        public string UserName { get; set; }

        [Required(ErrorMessage = null, ErrorMessageResourceName = "homeContactRequiredErrorMessage", ErrorMessageResourceType = typeof(ResWebsite))]
        [Display(Name = "homeContactEmail", ResourceType = typeof(ResWebsite))]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "homeContactPhone", ResourceType = typeof(ResWebsite))]
        public string Phone { get; set; }

        [Required(ErrorMessage = null, ErrorMessageResourceName = "homeContactRequiredErrorMessage", ErrorMessageResourceType = typeof(ResWebsite))]
        [Display(Name = "homeContactMessage", ResourceType = typeof(ResWebsite))]
        public string Message { get; set; }
    }
}
