using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication5.LocalDataModels;

namespace WebApplication5.Models
{
    public class mmsDBContext : DbContext
    {
        public mmsDBContext() : base("mmsDBContext")

        {



        }
        public DbSet<LocalMeetingPoll> LocalMeetingPolls { get; set; }
        public DbSet<LocalMeetingPollOptions> LocalMeetingPollOptions { get; set; }
        public DbSet<LocalPollVotting> LocalPollVotting { get; set; }
        public DbSet<Decision> Decisions { get; set; }
    }
}