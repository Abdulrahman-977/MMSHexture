using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class MeetingAgendaPollOption
    {
        public int pollOptionId { get; set; }
        public int pollId { get; set; }
        public string optionValue { get; set; }
    }
}
