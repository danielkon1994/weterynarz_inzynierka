using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Weterynarz.Basic.Resources;

namespace Weterynarz.Web.Models.AccountViewModels
{
    public class LoginViewModel
    {
        //[Required]
        //[EmailAddress]
        //public string Email { get; set; }

        [Required]
        [Display(Name = "loginViewModel_userName", ResourceType = typeof(ResAdmin))]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "loginViewModel_password", ResourceType = typeof(ResAdmin))]
        public string Password { get; set; }

        [Display(Name = "loginViewModel_rememberMe", ResourceType = typeof(ResAdmin))]
        public bool RememberMe { get; set; }
    }
}
