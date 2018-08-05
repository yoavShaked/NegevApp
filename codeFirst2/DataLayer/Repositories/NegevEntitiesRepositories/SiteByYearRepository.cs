using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace codeFirst2.DataLayer.Repositories.NegevEntitiesRepositories
{
    public class SiteByYearRepository : GenericRepository<SiteByYear>
    {
        public SiteByYearRepository(DbContext i_dbContext) : base(i_dbContext)
        {
        }

        public SiteByYearRepository()
        {

        }
    }
}