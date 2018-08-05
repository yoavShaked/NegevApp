using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace codeFirst2.DataLayer.LightWightesEntities
{
    public class LightWeightCoordinate: ILightWeight
    {
        public double X { get; set; }
        public double Y { get; set; }
        public int ID { get; set; }

        public LightWeightCoordinate(Coordinate i_Coordinate)
        {
            X = i_Coordinate.X;
            Y = i_Coordinate.Y;
            ID = i_Coordinate.ID;
        }
    }
}