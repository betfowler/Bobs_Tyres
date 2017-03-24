namespace Bobs_Tyres.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bobstyres : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LatestNews",
                c => new
                    {
                        LatestNewsID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Image = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.LatestNewsID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LatestNews");
        }
    }
}
