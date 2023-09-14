using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class MeetingExecutionAgendaTask
    {
        public int meetingExecutionAgendaTaskId { get; set; }
        public int meetingExecutionAgendaId { get; set; }
        public string taskDescription { get; set; }
        public int responsibleAttendeeId { get; set; }
        public DateTime taskTargetDate { get; set; }
        public int taskStatus { get; set; }
    }
}