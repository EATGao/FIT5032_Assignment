namespace HealingTalkNearestYou.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFileContent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UploadFiles", "FileContent", c => c.Binary(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UploadFiles", "FileContent");
        }
    }
}
