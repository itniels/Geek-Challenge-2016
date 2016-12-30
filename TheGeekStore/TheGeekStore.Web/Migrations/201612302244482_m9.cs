namespace TheGeekStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m9 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CustomerProfileModels", newName: "CustomerProfiles");
            RenameTable(name: "dbo.DailyDealModels", newName: "DailyDeals");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.DailyDeals", newName: "DailyDealModels");
            RenameTable(name: "dbo.CustomerProfiles", newName: "CustomerProfileModels");
        }
    }
}
