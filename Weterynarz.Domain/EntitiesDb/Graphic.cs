using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Weterynarz.Domain.EntitiesDb
{
    public class Graphic : BaseEntity
    {
        public int MondayFrom { get; set; }
        public int MondayTo { get; set; }
        public int TuesdayFrom { get; set; }
        public int TuesdayTo { get; set; }
        public int WednesdayFrom { get; set; }
        public int WednesdayTo { get; set; }
        public int ThursdayFrom { get; set; }
        public int ThursdayTo { get; set; }
        public int FridayFrom { get; set; }
        public int FridayTo { get; set; }
        public int SaturdayFrom { get; set; }
        public int SaturdayTo { get; set; }
        public int SundayFrom { get; set; }
        public int SundayTo { get; set; }

        [Required]
        public int DoctorGraphicId { get; set; }
        public DoctorGraphic DoctorGraphic { get; set; }
    }
}
