using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weterynarz.Web.Models.NotifyMessage;

namespace Weterynarz.Web.Services
{
    public interface INotifier
    {
        void SetMessage(string text, string optionalText, MessageStatus messageStatus);
    }
}
