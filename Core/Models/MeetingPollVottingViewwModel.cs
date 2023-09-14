using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class MeetingPollVottingViewwModel
    {
        public int meetingPollVotingId { get; set; }
        public int pollId { get; set; }
        public int pollOptionId { get; set; }
        public string userId { get; set; }
    }
}
