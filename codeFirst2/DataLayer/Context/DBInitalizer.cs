using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace codeFirst2.DataLayer.Context
{
    public class DBInitalizer:DropCreateDatabaseIfModelChanges<EntitiesNegev4>
    {
        protected override void Seed(EntitiesNegev4 context)
        {
            base.Seed(context);
            Layer layer = new Layer(2, "2017", 2017);
            context.Layers.Add(layer);
            context.SaveChanges();
        }
    }
}