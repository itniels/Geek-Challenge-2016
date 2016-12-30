namespace TheGeekStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Purchases", "FullName", c => c.String());
            AddColumn("dbo.Purchases", "EMail", c => c.String());
            AddColumn("dbo.Purchases", "PhoneNumber", c => c.String());
            AddColumn("dbo.Purchases", "AdrAtt", c => c.String());
            AddColumn("dbo.Purchases", "AdrStreet1", c => c.String());
            AddColumn("dbo.Purchases", "AdrStreet2", c => c.String());
            AddColumn("dbo.Purchases", "AdrPostal", c => c.String());
            AddColumn("dbo.Purchases", "AdrCity", c => c.String());
            AddColumn("dbo.Purchases", "AdrState", c => c.String());
            AddColumn("dbo.Purchases", "AdrCountry", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Purchases", "AdrCountry");
            DropColumn("dbo.Purchases", "AdrState");
            DropColumn("dbo.Purchases", "AdrCity");
            DropColumn("dbo.Purchases", "AdrPostal");
            DropColumn("dbo.Purchases", "AdrStreet2");
            DropColumn("dbo.Purchases", "AdrStreet1");
            DropColumn("dbo.Purchases", "AdrAtt");
            DropColumn("dbo.Purchases", "PhoneNumber");
            DropColumn("dbo.Purchases", "EMail");
            DropColumn("dbo.Purchases", "FullName");
        }
    }
}
