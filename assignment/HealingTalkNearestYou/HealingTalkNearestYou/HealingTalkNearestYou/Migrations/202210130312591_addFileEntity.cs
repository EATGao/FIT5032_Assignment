namespace HealingTalkNearestYou.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFileEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        FileId = c.Int(nullable: false),
                        FileName = c.String(nullable: false),
                        FilePath = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.Counsellings", t => t.FileId)
                .Index(t => t.FileId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Files", "FileId", "dbo.Counsellings");
            DropIndex("dbo.Files", new[] { "FileId" });
            DropTable("dbo.Files");
        }
    }
}
