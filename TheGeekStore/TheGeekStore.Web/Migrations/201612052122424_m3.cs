namespace TheGeekStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.News", "Exerpt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.News", "Exerpt", c => c.String());
        }
    }
}
