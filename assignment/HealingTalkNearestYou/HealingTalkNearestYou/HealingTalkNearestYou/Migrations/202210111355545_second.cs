namespace HealingTalkNearestYou.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Counsellings", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Counsellings", "ApplicationUser_Id");
            AddForeignKey("dbo.Counsellings", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Counsellings", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Counsellings", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Counsellings", "ApplicationUser_Id");
        }
    }
}
