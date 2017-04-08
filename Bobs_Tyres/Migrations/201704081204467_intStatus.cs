namespace Bobs_Tyres.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intStatus : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reviews", "StatusID", "dbo.ReviewStatus");
            DropIndex("dbo.Reviews", new[] { "StatusID" });
            AlterColumn("dbo.Reviews", "StatusID", c => c.Int(nullable: false));
            CreateIndex("dbo.Reviews", "StatusID");
            AddForeignKey("dbo.Reviews", "StatusID", "dbo.ReviewStatus", "StatusID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "StatusID", "dbo.ReviewStatus");
            DropIndex("dbo.Reviews", new[] { "StatusID" });
            AlterColumn("dbo.Reviews", "StatusID", c => c.Int());
            CreateIndex("dbo.Reviews", "StatusID");
            AddForeignKey("dbo.Reviews", "StatusID", "dbo.ReviewStatus", "StatusID");
        }
    }
}
