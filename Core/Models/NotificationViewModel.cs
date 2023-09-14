using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class NotificationViewModel
    {
        public int userNotificationId { get; set; }
        public int notificationId { get; set; }
        public string userId { get; set; }
        public bool isRead { get; set; }
        public DateTime lastActivityDateTime { get; set; }
        public string  notificationHeadline { get; set; }
        public string notificationText { get; set; }
    }
}
