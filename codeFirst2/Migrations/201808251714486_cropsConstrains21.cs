namespace codeFirst2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cropsConstrains21 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CropConstrains", "Crop1_Id", c => c.Int(nullable: false));
            AddColumn("dbo.CropConstrains", "Crop2_Id", c => c.Int(nullable: false));
            DropColumn("dbo.CropConstrains", "Crop1");
            DropColumn("dbo.CropConstrains", "Crop2");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CropConstrains", "Crop2", c => c.String());
            AddColumn("dbo.CropConstrains", "Crop1", c => c.String());
            DropColumn("dbo.CropConstrains", "Crop2_Id");
            DropColumn("dbo.CropConstrains", "Crop1_Id");
        }
    }
}
