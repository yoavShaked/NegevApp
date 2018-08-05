using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace codeFirst2.DataLayer.Repositories.NegevEntitiesRepositories
{
    public class CoordinateRepository : GenericRepository<Coordinate>
    {
        public CoordinateRepository(DbContext i_dbContext) : base(i_dbContext)
        {
        }

        public CoordinateRepository()
        {

        }
    }
}