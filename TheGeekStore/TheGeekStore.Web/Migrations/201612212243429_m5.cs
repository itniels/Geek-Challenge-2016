namespace TheGeekStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Purchases", "Cart_Id", "dbo.Carts");
            DropIndex("dbo.Purchases", new[] { "Cart_Id" });
            CreateTable(
                "dbo.CustomerProfileModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        FullName = c.String(),
                        PhoneNumber = c.String(),
                        AdrAtt = c.String(),
                        AdrStreet1 = c.String(),
                        AdrStreet2 = c.String(),
                        AdrPostal = c.Int(nullable: false),
                        AdrCity = c.String(),
                        AdrState = c.String(),
                        AdrCountry = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PurchaseItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Name = c.String(),
                        ProductNumber = c.String(),
                        Count = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        PurchaseModel_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Purchases", t => t.PurchaseModel_Id)
                .Index(t => t.PurchaseModel_Id);
            
            DropColumn("dbo.Purchases", "Price");
            DropColumn("dbo.Purchases", "AdrName");
            DropColumn("dbo.Purchases", "AdrAtt");
            DropColumn("dbo.Purchases", "AdrStreet");
            DropColumn("dbo.Purchases", "AdrPostal");
            DropColumn("dbo.Purchases", "AdrCity");
            DropColumn("dbo.Purchases", "AdrCountry");
            DropColumn("dbo.Purchases", "Cart_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Purchases", "Cart_Id", c => c.Int());
            AddColumn("dbo.Purchases", "AdrCountry", c => c.String());
            AddColumn("dbo.Purchases", "AdrCity", c => c.String());
            AddColumn("dbo.Purchases", "AdrPostal", c => c.Int(nullable: false));
            AddColumn("dbo.Purchases", "AdrStreet", c => c.String());
            AddColumn("dbo.Purchases", "AdrAtt", c => c.String());
            AddColumn("dbo.Purchases", "AdrName", c => c.String());
            AddColumn("dbo.Purchases", "Price", c => c.Double(nullable: false));
            DropForeignKey("dbo.PurchaseItems", "PurchaseModel_Id", "dbo.Purchases");
            DropIndex("dbo.PurchaseItems", new[] { "PurchaseModel_Id" });
            DropTable("dbo.PurchaseItems");
            DropTable("dbo.CustomerProfileModels");
            CreateIndex("dbo.Purchases", "Cart_Id");
            AddForeignKey("dbo.Purchases", "Cart_Id", "dbo.Carts", "Id");
        }
    }
}
