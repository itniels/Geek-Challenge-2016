namespace TheGeekStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m6 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CustomerProfileModels", "AdrPostal", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CustomerProfileModels", "AdrPostal", c => c.Int(nullable: false));
        }
    }
}
