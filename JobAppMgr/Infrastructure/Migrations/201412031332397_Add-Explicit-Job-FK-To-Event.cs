namespace Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExplicitJobFKToEvent : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "JobApplication_Id", "dbo.JobApplications");
            DropIndex("dbo.Events", new[] { "JobApplication_Id" });
            RenameColumn(table: "dbo.Events", name: "JobApplication_Id", newName: "JobApplicationId");
            AlterColumn("dbo.Events", "JobApplicationId", c => c.Int(nullable: false));
            CreateIndex("dbo.Events", "JobApplicationId");
            AddForeignKey("dbo.Events", "JobApplicationId", "dbo.JobApplications", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "JobApplicationId", "dbo.JobApplications");
            DropIndex("dbo.Events", new[] { "JobApplicationId" });
            AlterColumn("dbo.Events", "JobApplicationId", c => c.Int());
            RenameColumn(table: "dbo.Events", name: "JobApplicationId", newName: "JobApplication_Id");
            CreateIndex("dbo.Events", "JobApplication_Id");
            AddForeignKey("dbo.Events", "JobApplication_Id", "dbo.JobApplications", "Id");
        }
    }
}
