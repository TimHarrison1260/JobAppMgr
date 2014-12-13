namespace Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExplicitFKToJobApplications : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.JobApplications", name: "Company_Id", newName: "CompanyId");
            RenameIndex(table: "dbo.JobApplications", name: "IX_Company_Id", newName: "IX_CompanyId");
            //  Manually add FK to JobApplications to ensure the correct relationship with Companies
            AddForeignKey("dbo.JobApplications", "CompanyId", "dbo.Organisations");
        }
        
        public override void Down()
        {
            //  Manually remove FK between JobApplications and Companies
            DropForeignKey("dbo.JobApplications", "CompanyId", "dbo.Organisations");

            RenameIndex(table: "dbo.JobApplications", name: "IX_CompanyId", newName: "IX_Company_Id");
            RenameColumn(table: "dbo.JobApplications", name: "CompanyId", newName: "Company_Id");
        }
    }
}
