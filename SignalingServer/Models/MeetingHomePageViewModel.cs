using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class MeetingHomePageViewModel
    {
        public int meetingId { get; set; }
        public string time { get; set; }
        public string title { get; set; }
        public string userId { get; set; }
        public string UserName { get; set; }
        public Meeting meeting { get; set; }
        public int InvitationStatus { get; set; }
        
    }
}