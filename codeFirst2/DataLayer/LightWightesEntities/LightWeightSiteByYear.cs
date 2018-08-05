using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace codeFirst2.DataLayer.LightWightesEntities
{
    public class LightWeightSiteByYear: ILightWeight
    {
        public int ID { get; set; }
        public int LayerID { get; set; }
        public int SiteID { get; set; }
        public LightWeightCrop Crop { get; set; }

        public LightWeightSiteByYear(SiteByYear i_SiteByYear)
        {
            ID = i_SiteByYear.Id;
            LayerID = i_SiteByYear.CurrentLayer.Id;
            SiteID = i_SiteByYear.CurrentSite.Id;
            Crop = new LightWeightCrop(i_SiteByYear.CurrentCrop);
        }
    }
}