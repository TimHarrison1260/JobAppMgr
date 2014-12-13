namespace Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEventTypewithoutIdentityonkey : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EventTypes",
                c => new
                    {
                        Id = c.Int(nullable: false),
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
            
            AddColumn("dbo.Events", "EventTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Events", "EventTypeId");
            AddForeignKey("dbo.Events", "EventTypeId", "dbo.EventTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "EventTypeId", "dbo.EventTypes");
            DropForeignKey("dbo.EventTypes", "PrevId", "dbo.EventTypes");
            DropForeignKey("dbo.EventTypes", "NextId", "dbo.EventTypes");
            DropForeignKey("dbo.EventTypes", "CorrespondingStatus_Id", "dbo.Status");
            DropIndex("dbo.EventTypes", new[] { "CorrespondingStatus_Id" });
            DropIndex("dbo.EventTypes", new[] { "PrevId" });
            DropIndex("dbo.EventTypes", new[] { "NextId" });
            DropIndex("dbo.Events", new[] { "EventTypeId" });
            DropColumn("dbo.Events", "EventTypeId");
            DropTable("dbo.EventTypes");
        }
    }
}
