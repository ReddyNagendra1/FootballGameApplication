namespace FootballGameApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class players : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        PlayerID = c.Int(nullable: false, identity: true),
                        PlayerName = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PlayerID);
            
            AddColumn("dbo.AspNetUsers", "FavoriteColor", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "FavoriteColor");
            DropTable("dbo.Players");
        }
    }
}
