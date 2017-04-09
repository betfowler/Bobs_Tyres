namespace Bobs_Tyres.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class buttonLink : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LatestNews", "ButtonLink", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LatestNews", "ButtonLink");
        }
    }
}
