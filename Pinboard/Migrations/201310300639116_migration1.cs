namespace Pinboard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Boards",
                c => new
                    {
                        BoardId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.BoardId)
                .ForeignKey("dbo.UserProfile", t => t.User_UserId)
                .Index(t => t.User_UserId);
            
            AddColumn("dbo.Pins", "Url", c => c.String());
            AddColumn("dbo.Pins", "Board_BoardId", c => c.Int());
            AddForeignKey("dbo.Pins", "Board_BoardId", "dbo.Boards", "BoardId");
            CreateIndex("dbo.Pins", "Board_BoardId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Boards", new[] { "User_UserId" });
            DropIndex("dbo.Pins", new[] { "Board_BoardId" });
            DropForeignKey("dbo.Boards", "User_UserId", "dbo.UserProfile");
            DropForeignKey("dbo.Pins", "Board_BoardId", "dbo.Boards");
            DropColumn("dbo.Pins", "Board_BoardId");
            DropColumn("dbo.Pins", "Url");
            DropTable("dbo.Boards");
        }
    }
}
