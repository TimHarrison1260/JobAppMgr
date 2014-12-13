namespace Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveEventTypetoRemoveIdentity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EventTypes", "CorrespondingStatus_Id", "dbo.Status");
            DropForeignKey("dbo.EventTypes", "NextId", "dbo.EventTypes");
            DropForeignKey("dbo.EventTypes", "PrevId", "dbo.EventTypes");
            DropForeignKey("dbo.Events", "EventTypeId", "dbo.EventTypes");
            DropIndex("dbo.Events", new[] { "EventTypeId" });
            DropIndex("dbo.EventTypes", new[] { "NextId" });
            DropIndex("dbo.EventTypes", new[] { "PrevId" });
            DropIndex("dbo.EventTypes", new[] { "CorrespondingStatus_Id" });
            DropColumn("dbo.Events", "EventTypeId");
            DropTable("dbo.EventTypes");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Events", "EventTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.EventTypes", "CorrespondingStatus_Id");
            CreateIndex("dbo.EventTypes", "PrevId");
            CreateIndex("dbo.EventTypes", "NextId");
            CreateIndex("dbo.Events", "EventTypeId");
            AddForeignKey("dbo.Events", "EventTypeId", "dbo.EventTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.EventTypes", "PrevId", "dbo.EventTypes", "Id");
            AddForeignKey("dbo.EventTypes", "NextId", "dbo.EventTypes", "Id");
            AddForeignKey("dbo.EventTypes", "CorrespondingStatus_Id", "dbo.Status", "Id");
        }
    }
}
