namespace Rest_3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTitletoReviews : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Restaurant_Reviews", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Restaurant_Reviews", "Title");
        }
    }
}
