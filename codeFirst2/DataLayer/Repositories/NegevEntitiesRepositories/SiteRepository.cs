using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace codeFirst2.DataLayer.Repositories.NegevEntitiesRepositories
{
    public class SiteRepository : GenericRepository<Site>
    {
        public SiteRepository(DbContext i_dbContext) : base(i_dbContext)
        {
        }

        public SiteRepository()
        {

        }
    }
}