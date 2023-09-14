using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class MeetingResultViewModle
    {
        public int PollId { get; set; }
        
        public int PollOption { get; set; }
        public int Count { get; set; }
        public string PollTitle { get; set; }
        public string Polloptionvalue { get; set; }
    }
}