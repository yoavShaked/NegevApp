namespace codeFirst2.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<codeFirst2.DataLayer.EntitiesNegev4>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "codeFirst2.DataLayer.EntitiesNegev4";
        }

        protected override void Seed(codeFirst2.DataLayer.EntitiesNegev4 context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
