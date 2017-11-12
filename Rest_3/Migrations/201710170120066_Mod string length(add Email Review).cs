namespace Rest_3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModstringlengthaddEmailReview : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Restaurant_Reviews", "ReviewersEmail", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Restaurant_Reviews", "ReviewersEmail");
        }
    }
}
