namespace codeFirst2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.SiteByYears", name: "Site_Id", newName: "CurrentSite_Id");
            RenameColumn(table: "dbo.SiteByYears", name: "Layer_Id", newName: "CurrentLayer_Id");
            RenameIndex(table: "dbo.SiteByYears", name: "IX_Layer_Id", newName: "IX_CurrentLayer_Id");
            RenameIndex(table: "dbo.SiteByYears", name: "IX_Site_Id", newName: "IX_CurrentSite_Id");
            DropColumn("dbo.SiteByYears", "CurrentLayerId");
            DropColumn("dbo.SiteByYears", "CurrentSiteId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SiteByYears", "CurrentSiteId", c => c.Int(nullable: false));
            AddColumn("dbo.SiteByYears", "CurrentLayerId", c => c.Int(nullable: false));
            RenameIndex(table: "dbo.SiteByYears", name: "IX_CurrentSite_Id", newName: "IX_Site_Id");
            RenameIndex(table: "dbo.SiteByYears", name: "IX_CurrentLayer_Id", newName: "IX_Layer_Id");
            RenameColumn(table: "dbo.SiteByYears", name: "CurrentLayer_Id", newName: "Layer_Id");
            RenameColumn(table: "dbo.SiteByYears", name: "CurrentSite_Id", newName: "Site_Id");
        }
    }
}
