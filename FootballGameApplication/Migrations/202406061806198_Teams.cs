namespace FootballGameApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Teams : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        TeamID = c.Int(nullable: false, identity: true),
                        TeamName = c.String(),
                        Bio = c.String(),
                    })
                .PrimaryKey(t => t.TeamID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Teams");
        }
    }
}
