using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace codeFirst2.DataLayer.Repositories.NegevEntitiesRepositories
{
    public class CropsConstrainsRepository: GenericRepository<CropConstrains>
    {
        public CropsConstrainsRepository(DbContext i_dbContext) : base(i_dbContext)
        {
        }
    }
}