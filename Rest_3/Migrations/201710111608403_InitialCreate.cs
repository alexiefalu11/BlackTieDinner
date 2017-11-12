namespace Rest_3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cuisines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Restaurants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        City = c.String(nullable: false),
                        State = c.String(),
                        Zipcode = c.Int(nullable: false),
                        Email = c.String(),
                        Phone = c.String(),
                        Website = c.String(),
                        Price = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Restaurant_Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Comment = c.String(),
                        ReviewersName = c.String(maxLength: 15),
                        DateCreated = c.DateTime(nullable: false),
                        RestaurantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId, cascadeDelete: true)
                .Index(t => t.RestaurantId);
            
            CreateTable(
                "dbo.RestaurantsCuisines",
                c => new
                    {
                        Restaurants_Id = c.Int(nullable: false),
                        Cuisines_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Restaurants_Id, t.Cuisines_Id })
                .ForeignKey("dbo.Restaurants", t => t.Restaurants_Id, cascadeDelete: true)
                .ForeignKey("dbo.Cuisines", t => t.Cuisines_Id, cascadeDelete: true)
                .Index(t => t.Restaurants_Id)
                .Index(t => t.Cuisines_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Restaurant_Reviews", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.RestaurantsCuisines", "Cuisines_Id", "dbo.Cuisines");
            DropForeignKey("dbo.RestaurantsCuisines", "Restaurants_Id", "dbo.Restaurants");
            DropIndex("dbo.RestaurantsCuisines", new[] { "Cuisines_Id" });
            DropIndex("dbo.RestaurantsCuisines", new[] { "Restaurants_Id" });
            DropIndex("dbo.Restaurant_Reviews", new[] { "RestaurantId" });
            DropTable("dbo.RestaurantsCuisines");
            DropTable("dbo.Restaurant_Reviews");
            DropTable("dbo.Restaurants");
            DropTable("dbo.Cuisines");
        }
    }
}
