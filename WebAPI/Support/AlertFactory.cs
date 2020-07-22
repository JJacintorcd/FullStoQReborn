using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.HtmlComponents;

namespace WebAPI.Support
{
    public static class AlertFactory
    {
        public static string GenerateAlert(NotificationType type, string notification, string message)
        {
            return JsonConvert.SerializeObject(new AlertNotification() { Notification = notification, Type = type, Message = message });
        }


        public static string GenerateAlert(NotificationType type, string message)
        {
            return JsonConvert.SerializeObject(new AlertNotification() { Notification = type.ToString() + "!", Type = type, Message = message });
        }

        public static string GenerateAlert(NotificationType type, Exception exception)
        {
            return JsonConvert.SerializeObject(new AlertNotification() { Notification = type.ToString() + "!", Type = type, Message = exception.Message });
        }
    }
}
