namespace Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAttachmentToEvnt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "Attachment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "Attachment");
        }
    }
}
