using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class MeetingExecutionEndTime
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

    public class MeetingExecutionStartTime
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

    public class MeetingExecutionId
    {
        public int meetingExecutionId { get; set; }
        public int meetingId { get; set; }
        public DateTime meetingExecutionStartDate { get; set; }
        public MeetingExecutionStartTime meetingExecutionStartTime { get; set; }
        public MeetingExecutionEndTime meetingExecutionEndTime { get; set; }
        public int meetingExecutionStatus { get; set; }
    }
    
}