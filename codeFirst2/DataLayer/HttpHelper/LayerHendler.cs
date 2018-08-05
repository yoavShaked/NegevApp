using codeFirst2.DataLayer.Repositories.NegevEntitiesRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using codeFirst2.DataLayer.Repositories;
using System.Data.Entity;
using codeFirst2.DataLayer.LightWightesEntities;

namespace codeFirst2.DataLayer.HttpHelper
{
    public class LayerHendler : HttpHendlerRequestGeneric<Layer>
    {
        public LayerHendler(GenericRepository<Layer> repository) : base(repository)
        {
        }

        protected override ILightWeight GetLight(Layer i_Entity, DbContext i_Context)
        {
            i_Context.Entry(i_Entity).Collection(x => x.SiteByYears).Load();
            List<SiteByYear> sbys = i_Entity.SiteByYears;
            
            foreach(SiteByYear sby in sbys)
            {
                i_Context.Entry(sby).Reference(x => x.CurrentCrop).Load();
                i_Context.Entry(sby).Reference(x => x.CurrentLayer).Load();
                i_Context.Entry(sby).Reference(x => x.CurrentSite).Load();
            }

            return new LightWeightLayer(i_Entity);
        }
    }
}