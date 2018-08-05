using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace codeFirst2.DataLayer.Repositories.NegevEntitiesRepositories
{
    public class CropRepository : GenericRepository<Crop>
    {
        public CropRepository(DbContext i_dbContext) : base(i_dbContext)
        {
        }

        public CropRepository()
        {

        }
    }
}