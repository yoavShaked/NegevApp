namespace codeFirst2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmigrationCropsConstrains : DbMigration
    {
        public override void Up()
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CropContrains");
        }
    }
}
