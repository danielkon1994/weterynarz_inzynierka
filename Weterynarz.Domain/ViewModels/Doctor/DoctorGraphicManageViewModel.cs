using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Weterynarz.Domain.ViewModels.Doctor
{
    public class DoctorGraphicManageViewModel : BaseViewModel<int>
    {
        [Required]
        public string DoctorId { get; set; }

        public int GraphicId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data obowiązywania od")]
        public DateTime AvailableFrom { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data obowiązywania do")]
        public DateTime AvailableTo { get; set; }

        [Display(Name = "Poniedziałek od godziny")]
        public TimeSpan MondayFrom { get; set; }
        [Display(Name = "Poniedziałek do godziny")]
        public TimeSpan MondayTo { get; set; }
        [Display(Name = "Wtorek od godziny")]
        public TimeSpan TuesdayFrom { get; set; }
        [Display(Name = "Wtorek do godziny")]
        public TimeSpan TuesdayTo { get; set; }
        [Display(Name = "Środa od godziny")]
        public TimeSpan WednesdayFrom { get; set; }
        [Display(Name = "Środa do godziny")]
        public TimeSpan WednesdayTo { get; set; }
        [Display(Name = "Czwartek od godziny")]
        public TimeSpan ThursdayFrom { get; set; }
        [Display(Name = "Czwartek do godziny")]
        public TimeSpan ThursdayTo { get; set; }
        [Display(Name = "Piątek od godziny")]
        public TimeSpan FridayFrom { get; set; }
        [Display(Name = "Piątek do godziny")]
        public TimeSpan FridayTo { get; set; }
        [Display(Name = "Sobota od godziny")]
        public TimeSpan SaturdayFrom { get; set; }
        [Display(Name = "Sobota do godziny")]
        public TimeSpan SaturdayTo { get; set; }
        [Display(Name = "Niedziela od godziny")]
        public TimeSpan SundayFrom { get; set; }
        [Display(Name = "Niedziela do godziny")]
        public TimeSpan SundayTo { get; set; }
    }
}
