namespace Rest_3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ZipCodetoString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Restaurants", "Zipcode", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Restaurants", "Zipcode", c => c.Int(nullable: false));
        }
    }
}
