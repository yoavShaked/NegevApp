using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace codeFirst2.DataLayer.LightWightesEntities
{
    public class LightWeightSite: ILightWeight
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Dunam { get; set; }
        public string Region { get; set; }
        public List<LightWeightCoordinate> Coordinates { get; set; }

        public LightWeightSite(Site i_Site)
        {
            ID = i_Site.Id;
            Name = i_Site.Name;
            Dunam = i_Site.Dunam;
            Region = i_Site.Region;
            Coordinates = new List<LightWeightCoordinate>();

            foreach(Coordinate coordinate in i_Site.Coordinates)
            {
                Coordinates.Add(new LightWeightCoordinate(coordinate));
            }
        }
    }
}