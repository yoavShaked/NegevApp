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
    public class CropHendler : HttpHendlerRequestGeneric<Crop>
    {
        public CropHendler(GenericRepository<Crop> repository) : base(repository)
        {
        }

        protected override ILightWeight GetLight(Crop i_Crop, DbContext i_Context)
        {
            return new LightWeightCrop(i_Crop);
        }
    }
}