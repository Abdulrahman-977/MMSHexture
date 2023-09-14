using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication5.LocalDataModels
{
    public class LocalMeetingPoll
    {
        [Key]
        public int Id { get; set; }
        public int MeetingId { get; set; }
        public string PollQuestion { get; set; }
        public int status { get; set; }
    }
}