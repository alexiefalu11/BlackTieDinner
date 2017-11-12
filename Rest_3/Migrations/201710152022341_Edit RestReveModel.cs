namespace Rest_3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditRestReveModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RestaurantRatingModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zipcode = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        Website = c.String(),
                        Price = c.Int(nullable: false),
                        AvgRating = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RestaurantRatingModels");
        }
    }
}
