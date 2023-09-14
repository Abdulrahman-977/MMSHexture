using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class TaskViewModel
    {
        public int meetingNotesId { get; set; }
        public int meetingId { get; set; }
        public int meetingAgendaId { get; set; }
        public int notesType { get; set; }
        public string notes { get; set; }
        public int assignedToId { get; set; }
        public DateTime expectedCompletionDate { get; set; }
        public string defermentReason { get; set; }
    }
}
