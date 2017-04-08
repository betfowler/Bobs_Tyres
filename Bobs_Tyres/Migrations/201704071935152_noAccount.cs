namespace Bobs_Tyres.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class noAccount : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        userID = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.userID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Accounts");
        }
    }
}
