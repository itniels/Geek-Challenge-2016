namespace TheGeekStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m8 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DailyDealModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.Product_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DailyDealModels", "Product_Id", "dbo.Products");
            DropIndex("dbo.DailyDealModels", new[] { "Product_Id" });
            DropTable("dbo.DailyDealModels");
        }
    }
}
