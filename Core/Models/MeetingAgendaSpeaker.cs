using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class MeetingAgendaSpeaker
    {
        public int meetingAgendaSpeakerId { get; set; }
        public int meetingAgendaId { get; set; }
        public int meetingAttendeeId { get; set; }
        public string subject { get; set; }
        public string time { get; set; }
    }
}
