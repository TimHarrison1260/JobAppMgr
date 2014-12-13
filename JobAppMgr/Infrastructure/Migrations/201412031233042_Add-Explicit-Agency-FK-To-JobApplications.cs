namespace Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExplicitAgencyFKToJobApplications : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.JobApplications", name: "Agency_Id", newName: "AgencyId");
            RenameIndex(table: "dbo.JobApplications", name: "IX_Agency_Id", newName: "IX_AgencyId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.JobApplications", name: "IX_AgencyId", newName: "IX_Agency_Id");
            RenameColumn(table: "dbo.JobApplications", name: "AgencyId", newName: "Agency_Id");
        }
    }
}
