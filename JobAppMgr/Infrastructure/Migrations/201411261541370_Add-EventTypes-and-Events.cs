namespace Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEventTypesandEvents : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.String(),
                        Details = c.String(),
                        JobApplication_Id = c.Int(),
                        Type_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.JobApplications", t => t.JobApplication_Id)
                .ForeignKey("dbo.EventTypes", t => t.Type_Id)
                .Index(t => t.JobApplication_Id)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "dbo.EventTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Initial = c.Boolean(nullable: false),
                        NextId = c.Int(),
                        PrevId = c.Int(),
                        CorrespondingStatus_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Status", t => t.CorrespondingStatus_Id)
                .ForeignKey("dbo.EventTypes", t => t.NextId)
                .ForeignKey("dbo.EventTypes", t => t.PrevId)
                .Index(t => t.NextId)
                .Index(t => t.PrevId)
                .Index(t => t.CorrespondingStatus_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "Type_Id", "dbo.EventTypes");
            DropForeignKey("dbo.EventTypes", "PrevId", "dbo.EventTypes");
            DropForeignKey("dbo.EventTypes", "NextId", "dbo.EventTypes");
            DropForeignKey("dbo.EventTypes", "CorrespondingStatus_Id", "dbo.Status");
            DropForeignKey("dbo.Events", "JobApplication_Id", "dbo.JobApplications");
            DropIndex("dbo.EventTypes", new[] { "CorrespondingStatus_Id" });
            DropIndex("dbo.EventTypes", new[] { "PrevId" });
            DropIndex("dbo.EventTypes", new[] { "NextId" });
            DropIndex("dbo.Events", new[] { "Type_Id" });
            DropIndex("dbo.Events", new[] { "JobApplication_Id" });
            DropTable("dbo.EventTypes");
            DropTable("dbo.Events");
        }
    }
}
