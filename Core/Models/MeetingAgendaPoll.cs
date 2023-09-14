using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class MeetingAgendaPoll
    {
        public int pollId { get; set; }
    public int meetingId { get; set; }
    public string pollQuestion { get; set; }
    public int pollStatus { get; set; }
    }
}
