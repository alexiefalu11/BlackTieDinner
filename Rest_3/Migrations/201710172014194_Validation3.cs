namespace Rest_3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Validation3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Restaurants", "State", c => c.String(nullable: false));
            AlterColumn("dbo.Restaurants", "Zipcode", c => c.String(nullable: false, maxLength: 16));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Restaurants", "Zipcode", c => c.String());
            AlterColumn("dbo.Restaurants", "State", c => c.String());
        }
    }
}
