using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Weterynarz.Web.Models.NotifyMessage;
using Newtonsoft.Json;
using Weterynarz.Basic.Const;

namespace Weterynarz.Web.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {

        }

        public void NotifyMessage(string message, string optionalMessage, MessageStatus messageStatus)
        {
            Message tempMessage = new Message
            {
                MessageStatus = messageStatus,
                Text = message,
                OptionalText = optionalMessage
            };
            TempData[TempDataKey.Notification] = JsonConvert.SerializeObject(tempMessage);
        }
    }
}