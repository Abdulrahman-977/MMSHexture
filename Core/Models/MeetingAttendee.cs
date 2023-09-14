using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
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
        public string inviteStatusReason { get; set; }
        public int attendeeRole { get; set; }
        public string userId { get; set; }
        public int invitationType { get; set; }
    }
    
}
