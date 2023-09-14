using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class MeetingEndTime
    {
        public int ticks { get; set; }
        public int days { get; set; }
        public int hours { get; set; }
        public int milliseconds { get; set; }
        public int minutes { get; set; }
        public int seconds { get; set; }
        public int totalDays { get; set; }
        public int totalHours { get; set; }
        public int totalMilliseconds { get; set; }
        public int totalMinutes { get; set; }
        public int totalSeconds { get; set; }
    }

    public class MeetingStartTime
    {
        public int ticks { get; set; }
        public int days { get; set; }
        public int hours { get; set; }
        public int milliseconds { get; set; }
        public int minutes { get; set; }
        public int seconds { get; set; }
        public int totalDays { get; set; }
        public int totalHours { get; set; }
        public int totalMilliseconds { get; set; }
        public int totalMinutes { get; set; }
        public int totalSeconds { get; set; }
    }

    public class Meeting
    {
        public int meetingId { get; set; }
        public string meetingSubject { get; set; }
        public string meetingNumber { get; set; }
        public int boardId { get; set; }
        public DateTime meetingDate { get; set; }
        public MeetingStartTime meetingStartTime { get; set; }
        public MeetingEndTime meetingEndTime { get; set; }
        public int meetingStatus { get; set; }
        public bool meetingIsCancel { get; set; }
        public string meetingCancelReason { get; set; }
    }

    
}