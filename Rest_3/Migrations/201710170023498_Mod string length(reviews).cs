namespace Rest_3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modstringlengthreviews : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Restaurant_Reviews", "ReviewersName", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Restaurant_Reviews", "ReviewersName", c => c.String(maxLength: 15));
        }
    }
}
