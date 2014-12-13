namespace Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeFieldTypesinJobApplication : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.JobApplications", "CreateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.JobApplications", "ClosingDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.JobApplications", "ClosingDate", c => c.String());
            AlterColumn("dbo.JobApplications", "CreateDate", c => c.String());
        }
    }
}
