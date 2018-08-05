using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace codeFirst2.DataLayer.LightWightesEntities
{
    public class LightWeightLayer: ILightWeight
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public List<LightWeightSiteByYear> SitesByYears { get; set; }

        public LightWeightLayer(Layer i_Layer)
        {
            ID = i_Layer.Id;
            Name = i_Layer.Name;
            Year = i_Layer.Year;
            SitesByYears = new List<LightWeightSiteByYear>();

            foreach (SiteByYear sby in i_Layer.SiteByYears)
            {
                SitesByYears.Add(new LightWeightSiteByYear(sby));
            }
        }
    }
}