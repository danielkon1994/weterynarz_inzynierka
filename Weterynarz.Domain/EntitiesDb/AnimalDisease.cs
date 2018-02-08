using System;
using System.Collections.Generic;
using System.Text;

namespace Weterynarz.Domain.EntitiesDb
{
    public class AnimalDisease
    {
        public int AnimalId { get; set; }
        public Animal Animal { get; set; }
        public int DiseaseId { get; set; }
        public Disease Disease { get; set; }
    }
}
