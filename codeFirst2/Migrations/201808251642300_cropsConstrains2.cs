namespace codeFirst2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cropsConstrains2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CropConstrains",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Crop1 = c.String(),
                        Crop2 = c.String(),
                        NumOfYears = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.CropContrains");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CropContrains",
                c => new
                    {
                        Crop1 = c.String(nullable: false, maxLength: 128),
                        Crop2 = c.String(),
                        NumOfYears = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Crop1);
            
            DropTable("dbo.CropConstrains");
        }
    }
}
