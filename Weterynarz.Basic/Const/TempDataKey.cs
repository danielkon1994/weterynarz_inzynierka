using System;
using System.Collections.Generic;
using System.Text;

namespace Weterynarz.Basic.Const
{
    public static class TempDataKey
    {
        public readonly static string Notification;

        static TempDataKey()
        {
            Notification = Guid.NewGuid().ToString();
        }
    }
}
