using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class MeetingExecutionAgendaPollVoting
    {
        public int meetingExecutionAgendaPollVotingId { get; set; }
        public int pollId { get; set; }
        public int pollOptionId { get; set; }
        public int meetingExecutionAttendeeId { get; set; }
    }
}