namespace Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompanyAgencyContactRenameDetailstoOrganisation : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Agencies", newName: "Organisations");
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.String(),
                        Organisation_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organisations", t => t.Organisation_Id)
                .Index(t => t.Organisation_Id);
            
            AddColumn("dbo.Organisations", "Type", c => c.String(nullable: false, maxLength: 128));

            //  Manual edit to remove FK to Companies table from JobApplications table
            DropForeignKey("dbo.JobApplications", "Company_Id", "dbo.Companies");

            DropTable("dbo.Companies");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        PostCode = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                ;

            //  Manual edit to reinstate FK to Companies table from JobApplications table
            AddForeignKey("dbo.JobApplications", "Company_Id", "dbo.Companies");

            DropForeignKey("dbo.Contacts", "Organisation_Id", "dbo.Organisations");
            DropIndex("dbo.Contacts", new[] { "Organisation_Id" });
            DropColumn("dbo.Organisations", "Type");
            DropTable("dbo.Contacts");
            RenameTable(name: "dbo.Organisations", newName: "Agencies");
        }
    }
}
