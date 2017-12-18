using System;
using System.Collections.Generic;
using System.Text;

namespace Weterynarz.Services.ViewModels
{
    public class BaseViewModel<T>
    {
        public T Id { get; set; }

        public bool Active { get; set; }

        public bool Deleted { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? ModificationDate { get; set; }
    }
}
