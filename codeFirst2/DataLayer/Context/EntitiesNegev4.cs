using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace codeFirst2.DataLayer
{
    public class EntitiesNegev4 : DbContext
    {
        public EntitiesNegev4() : base("DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Layer> Layers { get; set; }
        public DbSet<SiteByYear> SiteByYears { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<Crop> Crops { get; set; }
        public DbSet<Coordinate> Coordinates { get; set; }
        public DbSet<CropConstrains> CropConstrains { get; set; }
    }
}