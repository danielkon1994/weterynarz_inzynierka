
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Weterynarz.Domain.EntitiesDb
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Animals = new Collection<Animal>();
        }

        public string Name { get; set; }

        public string Surname { get; set; }

        public virtual ICollection<Animal> Animals { get; set; }

        //Additional data
        public string Address { get; set; }
        public string HouseNumber { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
    }
}
