using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class MeetingAttendeeViewModel
    {
        public int Id { get; set; }
        public string MeetingName { get; set; }
        public string Date { get; set; }
        public int status { get; set; }
        public string boardname { get; set; }
        public string roomname { get; set; }
        public Meeting meeting { get; set; }
        public List<MeetingAttendee> Attendees { get; set; }
        public List<MeetingAgendaSpeaker> Speakers { get; set; }
        public MeetingAttendee Attendee { get; set; }
    }
}