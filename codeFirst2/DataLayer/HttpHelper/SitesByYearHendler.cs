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
    public class SitesByYearHendler : HttpHendlerRequestGeneric<SiteByYear>
    {
        public SitesByYearHendler(GenericRepository<SiteByYear> repository) : base(repository)
        {
        }

        protected override ILightWeight GetLight(SiteByYear i_SiteByYear, DbContext i_Context)
        {
            i_Context.Entry(i_SiteByYear).Reference(x => x.CurrentCrop).Load();
            i_Context.Entry(i_SiteByYear).Reference(x => x.CurrentLayer).Load();
            i_Context.Entry(i_SiteByYear).Reference(x => x.CurrentSite).Load();

            return new LightWeightSiteByYear(i_SiteByYear);
        }

    }
}