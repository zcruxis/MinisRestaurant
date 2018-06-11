namespace MinisBack.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameQuantityFieldInOrderItemEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderItem", "Quantity", c => c.Int(nullable: false));
            DropColumn("dbo.OrderItem", "Quantiy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderItem", "Quantiy", c => c.Int(nullable: false));
            DropColumn("dbo.OrderItem", "Quantity");
        }
    }
}
