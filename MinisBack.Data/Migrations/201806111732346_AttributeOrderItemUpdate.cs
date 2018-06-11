namespace MinisBack.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AttributeOrderItemUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderItem", "Cut", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderItem", "Cut");
        }
    }
}
