using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weterynarz.Web.Models.NotifyMessage
{
    public class Message
    {
        public string Text { get; set; }

        public string OptionalText { get; set; }

        public MessageStatus MessageStatus { get; set; }
    }

    public enum MessageStatus
    {
        error = 1,
        success = 2,
        warning = 3,
        info = 4,
        question = 5
    }
}
