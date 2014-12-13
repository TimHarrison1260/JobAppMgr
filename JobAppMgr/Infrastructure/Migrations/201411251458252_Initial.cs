namespace Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JobApplications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        CreateDate = c.String(),
                        ClosingDate = c.String(),
                        Location = c.String(),
                        Description = c.String(),
                        KeySkills = c.String(),
                        NiceToHaveSkills = c.String(),
                        Benefits = c.String(),
                        Conditions = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        Agency_Id = c.Int(),
                        Company_Id = c.Int(),
                        Status_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Agencies", t => t.Agency_Id)
                .ForeignKey("dbo.Companies", t => t.Company_Id)
                .ForeignKey("dbo.Status", t => t.Status_Id)
                .Index(t => t.Agency_Id)
                .Index(t => t.Company_Id)
                .Index(t => t.Status_Id);
            
            CreateTable(
                "dbo.Agencies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        PostCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        PostCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobApplications", "Status_Id", "dbo.Status");
            DropForeignKey("dbo.JobApplications", "Company_Id", "dbo.Companies");
            DropForeignKey("dbo.JobApplications", "Agency_Id", "dbo.Agencies");
            DropIndex("dbo.JobApplications", new[] { "Status_Id" });
            DropIndex("dbo.JobApplications", new[] { "Company_Id" });
            DropIndex("dbo.JobApplications", new[] { "Agency_Id" });
            DropTable("dbo.Status");
            DropTable("dbo.Companies");
            DropTable("dbo.Agencies");
            DropTable("dbo.JobApplications");
        }
    }
}
