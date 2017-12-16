using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weterynarz.Web.Models.NotifyMessage;

namespace Weterynarz.Web.Services
{
    public class Notifier : INotifier
    {
        public Message Message { get; set; }
        
        public void SetMessage(string text, string optionalText, MessageStatus messageStatus)
        {
            Message = new Message { Text = text, OptionalText = optionalText, MessageStatus = messageStatus };
        }
    }
}
