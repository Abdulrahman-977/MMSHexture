using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication5.LocalDataModels
{
    public class LocalMeetingPollOptions
    {
        [Key]
        public int Id { get; set; }
        public int PollId { get; set; }
        public string OptionValue { get; set; }
    }
}