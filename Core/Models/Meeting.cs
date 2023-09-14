using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Meeting
    {
        public int meetingId { get; set; }
        public string meetingSubject { get; set; }
        public string meetingNumber { get; set; }
        public int boardId { get; set; }
        public DateTime meetingDate { get; set; }
        public string meetingStartTime { get; set; }
        public string meetingEndTime { get; set; }
        public int meetingStatus { get; set; }
        public bool meetingIsCancel { get; set; }
        public string meetingCancelReason { get; set; }
        public int referenceNumber { get; set; }
        public int degreeOfImportance { get; set; }
        public string meetingType { get; set; }
        public string meetingComment { get; set; }
        public int? roomId { get; set; }
        public string createdBy { get; set; }
    }
}
