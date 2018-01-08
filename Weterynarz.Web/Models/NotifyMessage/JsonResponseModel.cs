using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weterynarz.Web.Models.NotifyMessage
{
    public class JsonResponseModel
    {
        public string Message { get; set; }
        public MessageStatus Status { get; set; }
    }
}
