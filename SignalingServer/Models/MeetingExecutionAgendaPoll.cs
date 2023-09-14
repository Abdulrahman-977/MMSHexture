using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class MeetingExecutionAgendaPoll
    {
        public int pollId { get; set; }
        public int meetingExecutionAgendaId { get; set; }
        public string pollName { get; set; }
        public int pollStatus { get; set; }
        public DateTime pollStartdatetime { get; set; }
        public DateTime pollEnddatetime { get; set; }
    }
}