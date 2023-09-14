using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class MeetingExecutionAgendaRaisehand
    {
        public int raisehandId { get; set; }
        public int meetingExecutionAgendaId { get; set; }
        public int meetingExecutionAttendeeId { get; set; }
        public int raisehandStatus { get; set; }
    }
}