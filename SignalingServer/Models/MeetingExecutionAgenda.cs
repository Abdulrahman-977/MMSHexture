using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class MeetingExecutionAgenda
    {
        public int meetingExecutionAgendaId { get; set; }
        public int meetingExecutionId { get; set; }
        public int meetingAgendaId { get; set; }
        public int meetingExecutionAgendaStatus { get; set; }
        public int meetingAgendaSpeakerId { get; set; }
    }
}