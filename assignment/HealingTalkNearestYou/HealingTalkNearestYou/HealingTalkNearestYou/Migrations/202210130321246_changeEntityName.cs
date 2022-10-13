namespace HealingTalkNearestYou.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeEntityName : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Files", newName: "UploadFiles");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.UploadFiles", newName: "Files");
        }
    }
}
