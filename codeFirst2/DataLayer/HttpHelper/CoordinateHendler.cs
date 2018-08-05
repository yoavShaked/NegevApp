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
    public class CoordinateHendler : HttpHendlerRequestGeneric<Coordinate>
    {
        public CoordinateHendler(GenericRepository<Coordinate> repository) : base(repository)
        {
        }

        protected override ILightWeight GetLight(Coordinate i_Coordinate, DbContext i_Context)
        {
            return new LightWeightCoordinate(i_Coordinate);
        }
    }
}