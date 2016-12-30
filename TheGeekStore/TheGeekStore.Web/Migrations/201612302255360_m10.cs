namespace TheGeekStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DailyDeals", "ProductOriginalPrice", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DailyDeals", "ProductOriginalPrice");
        }
    }
}
