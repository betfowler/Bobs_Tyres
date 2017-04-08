namespace Bobs_Tyres.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reviewstatus : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReviewStatus",
                c => new
                    {
                        StatusID = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.StatusID);
            
            AddColumn("dbo.Reviews", "StatusID", c => c.Int());
            CreateIndex("dbo.Reviews", "StatusID");
            AddForeignKey("dbo.Reviews", "StatusID", "dbo.ReviewStatus", "StatusID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "StatusID", "dbo.ReviewStatus");
            DropIndex("dbo.Reviews", new[] { "StatusID" });
            DropColumn("dbo.Reviews", "StatusID");
            DropTable("dbo.ReviewStatus");
        }
    }
}
