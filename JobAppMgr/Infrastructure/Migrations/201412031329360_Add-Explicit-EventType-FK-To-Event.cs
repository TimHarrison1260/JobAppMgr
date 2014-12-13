namespace Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExplicitEventTypeFKToEvent : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "Type_Id", "dbo.EventTypes");
            DropIndex("dbo.Events", new[] { "Type_Id" });
            RenameColumn(table: "dbo.Events", name: "Type_Id", newName: "EventTypeId");
            AlterColumn("dbo.Events", "EventTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Events", "EventTypeId");
            AddForeignKey("dbo.Events", "EventTypeId", "dbo.EventTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "EventTypeId", "dbo.EventTypes");
            DropIndex("dbo.Events", new[] { "EventTypeId" });
            AlterColumn("dbo.Events", "EventTypeId", c => c.Int());
            RenameColumn(table: "dbo.Events", name: "EventTypeId", newName: "Type_Id");
            CreateIndex("dbo.Events", "Type_Id");
            AddForeignKey("dbo.Events", "Type_Id", "dbo.EventTypes", "Id");
        }
    }
}
