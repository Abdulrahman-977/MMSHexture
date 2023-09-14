using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    
    public class MeetingAgendaAttachment
    {
        public int meetingAgendaAttachmentId { get; set; }
        public int meetingAgendaId { get; set; }
        public int attachmentNumber { get; set; }
        public string fileName { get; set; }
    }

    public class MeetingAgenda
    {
        public int meetingAgendaId { get; set; }
        public int meetingId { get; set; }
        public string agendaTitle { get; set; }
        public string agendaDescription { get; set; }
        public int meetingAgendaSpeakerId { get; set; }
        public List<MeetingAgendaAttachment> meetingAgendaAttachments { get; set; }
    }
}
