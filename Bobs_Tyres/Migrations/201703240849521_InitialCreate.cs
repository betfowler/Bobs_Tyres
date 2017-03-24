namespace Bobs_Tyres.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Date = c.DateTime(),
                        Rating = c.Int(nullable: false),
                        Testimonial = c.String(),
                        Location = c.String(),
                    })
                .PrimaryKey(t => t.ReviewID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Reviews");
        }
    }
}
