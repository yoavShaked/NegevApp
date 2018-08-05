namespace codeFirst2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Coordinates",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Y = c.Double(nullable: false),
                        X = c.Double(nullable: false),
                        Site_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sites", t => t.Site_Id)
                .Index(t => t.Site_Id);
            
            CreateTable(
                "dbo.Sites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Dunam = c.Int(nullable: false),
                        Region = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SiteByYears",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CurrentLayerId = c.Int(nullable: false),
                        CurrentSiteId = c.Int(nullable: false),
                        CurrentCrop_ID = c.Int(),
                        Site_Id = c.Int(),
                        Layer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Crops", t => t.CurrentCrop_ID)
                .ForeignKey("dbo.Sites", t => t.Site_Id)
                .ForeignKey("dbo.Layers", t => t.Layer_Id)
                .Index(t => t.CurrentCrop_ID)
                .Index(t => t.Site_Id)
                .Index(t => t.Layer_Id);
            
            CreateTable(
                "dbo.Crops",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Layers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Year = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SiteByYears", "Layer_Id", "dbo.Layers");
            DropForeignKey("dbo.SiteByYears", "Site_Id", "dbo.Sites");
            DropForeignKey("dbo.SiteByYears", "CurrentCrop_ID", "dbo.Crops");
            DropForeignKey("dbo.Coordinates", "Site_Id", "dbo.Sites");
            DropIndex("dbo.SiteByYears", new[] { "Layer_Id" });
            DropIndex("dbo.SiteByYears", new[] { "Site_Id" });
            DropIndex("dbo.SiteByYears", new[] { "CurrentCrop_ID" });
            DropIndex("dbo.Coordinates", new[] { "Site_Id" });
            DropTable("dbo.Layers");
            DropTable("dbo.Crops");
            DropTable("dbo.SiteByYears");
            DropTable("dbo.Sites");
            DropTable("dbo.Coordinates");
        }
    }
}
