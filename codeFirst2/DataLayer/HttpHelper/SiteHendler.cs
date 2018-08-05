using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using codeFirst2.DataLayer.LightWightesEntities;
using codeFirst2.DataLayer.Repositories;
using codeFirst2.DataLayer.Repositories.NegevEntitiesRepositories;

namespace codeFirst2.DataLayer.HttpHelper
{
    public class SiteHendler : HttpHendlerRequestGeneric<Site>
    {
        public SiteHendler(GenericRepository<Site> repository) : base(repository)
        {
        }

        protected override ILightWeight GetLight(Site i_Site, DbContext i_Context)
        {
            i_Context.Entry(i_Site).Collection(x => x.Coordinates).Load();

            return new LightWeightSite(i_Site);
        }
    }
}