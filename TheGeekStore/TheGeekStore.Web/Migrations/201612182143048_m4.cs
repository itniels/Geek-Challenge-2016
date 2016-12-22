namespace TheGeekStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CartItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Count = c.Int(nullable: false),
                        Cart_Id = c.Int(),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Carts", t => t.Cart_Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.Cart_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        LastAccessed = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        PurchaseDate = c.DateTime(nullable: false),
                        PromoCode = c.String(),
                        Price = c.Double(nullable: false),
                        PriceShipping = c.Double(nullable: false),
                        AdrName = c.String(),
                        AdrAtt = c.String(),
                        AdrStreet = c.String(),
                        AdrPostal = c.Int(nullable: false),
                        AdrCity = c.String(),
                        AdrCountry = c.String(),
                        Cart_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Carts", t => t.Cart_Id)
                .Index(t => t.Cart_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Purchases", "Cart_Id", "dbo.Carts");
            DropForeignKey("dbo.CartItems", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.CartItems", "Cart_Id", "dbo.Carts");
            DropIndex("dbo.Purchases", new[] { "Cart_Id" });
            DropIndex("dbo.CartItems", new[] { "Product_Id" });
            DropIndex("dbo.CartItems", new[] { "Cart_Id" });
            DropTable("dbo.Purchases");
            DropTable("dbo.Carts");
            DropTable("dbo.CartItems");
        }
    }
}
