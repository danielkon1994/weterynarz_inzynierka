using Microsoft.AspNetCore.Identity;
using System;
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
            DoctorGraphics = new Collection<DoctorGraphic>();
            Visits = new Collection<Visit>();
        }

        public string Name { get; set; }

        public string Surname { get; set; }

        public ICollection<Animal> Animals { get; set; }

        public ICollection<IdentityUserRole<string>> Roles { get; set; }

        public ICollection<DoctorGraphic> DoctorGraphics { get; set; }

        public ICollection<Visit> Visits { get; set; }

        //Additional data
        public string DoctorSpecialization { get; set; }

        public string Address { get; set; }
        public string HouseNumber { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public bool Active { get; set; }

        public bool Deleted { get; set; }
    }
}
