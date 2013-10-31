namespace Pinboard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pins", "Board_BoardId", "dbo.Boards");
            DropIndex("dbo.Pins", new[] { "Board_BoardId" });
            RenameColumn(table: "dbo.Pins", name: "Board_BoardId", newName: "BoardId");
            AddForeignKey("dbo.Pins", "BoardId", "dbo.Boards", "BoardId", cascadeDelete: true);
            CreateIndex("dbo.Pins", "BoardId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Pins", new[] { "BoardId" });
            DropForeignKey("dbo.Pins", "BoardId", "dbo.Boards");
            RenameColumn(table: "dbo.Pins", name: "BoardId", newName: "Board_BoardId");
            CreateIndex("dbo.Pins", "Board_BoardId");
            AddForeignKey("dbo.Pins", "Board_BoardId", "dbo.Boards", "BoardId");
        }
    }
}
