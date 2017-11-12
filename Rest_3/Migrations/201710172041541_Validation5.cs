namespace Rest_3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Validation5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Restaurants", "Phone", c => c.String(nullable: false, maxLength: 16));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Restaurants", "Phone", c => c.String(maxLength: 16));
        }
    }
}
