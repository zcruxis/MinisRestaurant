namespace MinisBack.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class minisdb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ingredient",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sandwich",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        Price = c.Single(nullable: false),
                        SandwichTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SandwichId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        SandwichPrice = c.Single(nullable: false),
                        TotalPrice = c.Single(nullable: false),
                        OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Order", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Single(nullable: false),
                        Paid = c.Boolean(nullable: false),
                        Completed = c.Boolean(nullable: false),
                        DateMade = c.DateTime(nullable: false),
                        DatePaid = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SandwichType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        Cut = c.Byte(nullable: false),
                        Salsa = c.Byte(nullable: false),
                        Compressed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SandwichIngredient",
                c => new
                    {
                        SandwichId = c.Int(nullable: false),
                        IngredientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SandwichId, t.IngredientId })
                .ForeignKey("dbo.Sandwich", t => t.SandwichId, cascadeDelete: true)
                .ForeignKey("dbo.Ingredient", t => t.IngredientId, cascadeDelete: true)
                .Index(t => t.SandwichId)
                .Index(t => t.IngredientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderItem", "OrderId", "dbo.Order");
            DropForeignKey("dbo.SandwichIngredient", "IngredientId", "dbo.Ingredient");
            DropForeignKey("dbo.SandwichIngredient", "SandwichId", "dbo.Sandwich");
            DropIndex("dbo.SandwichIngredient", new[] { "IngredientId" });
            DropIndex("dbo.SandwichIngredient", new[] { "SandwichId" });
            DropIndex("dbo.OrderItem", new[] { "OrderId" });
            DropTable("dbo.SandwichIngredient");
            DropTable("dbo.SandwichType");
            DropTable("dbo.Order");
            DropTable("dbo.OrderItem");
            DropTable("dbo.Sandwich");
            DropTable("dbo.Ingredient");
        }
    }
}
