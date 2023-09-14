namespace WebApplication5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newMig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Decisions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        meetingId = c.Int(nullable: false),
                        BoardId = c.Int(nullable: false),
                        Title = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LocalMeetingPollOptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PollId = c.Int(nullable: false),
                        OptionValue = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LocalMeetingPolls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MeetingId = c.Int(nullable: false),
                        PollQuestion = c.String(),
                        status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LocalPollVottings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PollId = c.Int(nullable: false),
                        PollOptionId = c.Int(nullable: false),
                        userId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LocalPollVottings");
            DropTable("dbo.LocalMeetingPolls");
            DropTable("dbo.LocalMeetingPollOptions");
            DropTable("dbo.Decisions");
        }
    }
}
