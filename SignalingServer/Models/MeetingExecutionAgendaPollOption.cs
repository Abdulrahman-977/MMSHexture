using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class MeetingExecutionAgendaPollOption
    {
        public int pollOptionId { get; set; }
        public int pollId { get; set; }
        public string pollOptionDescription { get; set; }
    }
}