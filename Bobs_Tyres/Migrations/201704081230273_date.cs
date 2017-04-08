namespace Bobs_Tyres.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class date : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reviews", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reviews", "Date", c => c.DateTime());
        }
    }
}
