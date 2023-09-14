using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class MeetingAttendee
    {
        public int meetingAttendeeId { get; set; }
        public int meetingId { get; set; }
        public string attendeeName { get; set; }
        public string attendeeOrganization { get; set; }
        public string attendeeDesignation { get; set; }
        public string attendeeEmail { get; set; }
        public string attendeeMobile { get; set; }
        public int inviteStatus { get; set; }
        public int userId { get; set; }
    }
}