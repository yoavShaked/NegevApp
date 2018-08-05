using codeFirst2.DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace codeFirst2.DataLayer
{
    public class Coordinate: ITableEntity
    {
        [Key]
        public int ID { get; set; }
        public double Y  { get; set; }
        public double X { get; set; }
        public Site Site { get; set; }

        public Coordinate(double i_X, double i_Y, int i_ID, Site i_Site)
        {
            X = i_X;
            Y = i_Y;
            Site = i_Site;
            ID = i_ID;
        }

        public Coordinate()
        {

        }
    }
}