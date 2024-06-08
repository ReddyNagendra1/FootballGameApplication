namespace FootballGameApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class venueplayers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Venues",
                c => new
                    {
                        VenueID = c.Int(nullable: false, identity: true),
                        VenueName = c.String(),
                        Location = c.String(),
                    })
                .PrimaryKey(t => t.VenueID);
            
            CreateTable(
                "dbo.VenuePlayers",
                c => new
                    {
                        Venue_VenueID = c.Int(nullable: false),
                        Player_PlayerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Venue_VenueID, t.Player_PlayerID })
                .ForeignKey("dbo.Venues", t => t.Venue_VenueID, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.Player_PlayerID, cascadeDelete: true)
                .Index(t => t.Venue_VenueID)
                .Index(t => t.Player_PlayerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VenuePlayers", "Player_PlayerID", "dbo.Players");
            DropForeignKey("dbo.VenuePlayers", "Venue_VenueID", "dbo.Venues");
            DropIndex("dbo.VenuePlayers", new[] { "Player_PlayerID" });
            DropIndex("dbo.VenuePlayers", new[] { "Venue_VenueID" });
            DropTable("dbo.VenuePlayers");
            DropTable("dbo.Venues");
        }
    }
}
