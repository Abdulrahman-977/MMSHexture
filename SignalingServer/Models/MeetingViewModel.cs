using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Models
{
    public class MeetingViewModel
    {
        public int meetingId { get; set; }
        public string meetingSubject { get; set; }
        public string meetingNumber { get; set; }
        public int boardId { get; set; }
        public string boardName { get; set; }
        public string roomName { get; set; }
        public DateTime meetingDate { get; set; }
        public string meetingStartTime { get; set; }
        public string meetingEndTime { get; set; }
        public int meetingStatus { get; set; }
        public int degreeOfImportance { get; set; }
        public string meetingType { get; set; }
        public List<MeetingAttendee> Attendees { get; set; }
        public List<MeetingAgendaSpeaker> MeetingAgendaSpeakers { get; set; }
        public List<MeetingAgendaAttachment> meetingAgendaAttachments { get; set; }
    }
}
