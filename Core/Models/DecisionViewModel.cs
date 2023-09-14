using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class DecisionViewModel
    {
        public int decisionId { get; set; }
        public int meetingId { get; set; }
        public int boardId { get; set; }
        public string title { get; set; }
        public DateTime startDate { get; set; }
        public DateTime createdDate { get; set; }
    }
}
