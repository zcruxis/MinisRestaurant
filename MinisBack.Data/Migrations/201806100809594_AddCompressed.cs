namespace MinisBack.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompressed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SandwichType", "Compressed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SandwichType", "Compressed");
        }
    }
}
