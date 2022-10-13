namespace HealingTalkNearestYou.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addEndDateTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Counsellings", "CEndDateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Counsellings", "CEndDateTime");
        }
    }
}
