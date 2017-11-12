namespace Rest_3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Validation4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Restaurants", "Zipcode", c => c.String(nullable: false, maxLength: 11));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Restaurants", "Zipcode", c => c.String(nullable: false, maxLength: 16));
        }
    }
}
