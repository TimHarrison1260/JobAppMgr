namespace Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Synchronisemodelanddatabase : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EventTypes", "NextId", "dbo.EventTypes");
            DropForeignKey("dbo.EventTypes", "PrevId", "dbo.EventTypes");
            DropForeignKey("dbo.Events", "EventTypeId", "dbo.EventTypes");
            DropPrimaryKey("dbo.EventTypes");
            AlterColumn("dbo.EventTypes", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.EventTypes", "Id");
            AddForeignKey("dbo.EventTypes", "NextId", "dbo.EventTypes", "Id");
            AddForeignKey("dbo.EventTypes", "PrevId", "dbo.EventTypes", "Id");
            AddForeignKey("dbo.Events", "EventTypeId", "dbo.EventTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "EventTypeId", "dbo.EventTypes");
            DropForeignKey("dbo.EventTypes", "PrevId", "dbo.EventTypes");
            DropForeignKey("dbo.EventTypes", "NextId", "dbo.EventTypes");
            DropPrimaryKey("dbo.EventTypes");
            AlterColumn("dbo.EventTypes", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.EventTypes", "Id");
            AddForeignKey("dbo.Events", "EventTypeId", "dbo.EventTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.EventTypes", "PrevId", "dbo.EventTypes", "Id");
            AddForeignKey("dbo.EventTypes", "NextId", "dbo.EventTypes", "Id");
        }
    }
}
