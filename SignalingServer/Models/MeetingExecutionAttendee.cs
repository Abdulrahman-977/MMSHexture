using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class MeetingExecutionAttendee
    {
        public int meetingExecutionAttendeeId { get; set; }
        public int meetingExecutionId { get; set; }
        public int meetingAttendeeId { get; set; }
        public int meetingExecutionAttendeeStatus { get; set; }
    }
}