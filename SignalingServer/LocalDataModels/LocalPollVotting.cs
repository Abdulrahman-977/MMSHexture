using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication5.LocalDataModels
{
    public class LocalPollVotting
    {
        [Key]
        public int Id { get; set; }
        public int PollId { get; set; }

        public int PollOptionId { get; set; }
        public string userId { get; set; }
    }
}