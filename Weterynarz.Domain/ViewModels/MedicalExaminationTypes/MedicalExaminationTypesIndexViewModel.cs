﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Weterynarz.Domain.ViewModels.MedicalExaminationTypes
{
    public class MedicalExaminationTypesIndexViewModel : BaseViewModel<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}