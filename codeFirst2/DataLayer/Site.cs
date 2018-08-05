using codeFirst2.DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace codeFirst2.DataLayer
{
    public class Site: ITableEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Dunam { get; set; }
        public string Region { get; set; }
        public List<Coordinate> Coordinates { get; set; }
        public List<SiteByYear> SiteByYears { get; set; }

        public Site(int i_ID, int i_Dunam, string i_Name, string i_Region, List<Coordinate> i_Coordinates, List<SiteByYear> i_SitesByYears)
        {
            Id = i_ID;
            Name = i_Name;
            Dunam = i_Dunam;
            Region = i_Region;
        }

        public Site()
        {
            Coordinates = new List<Coordinate>();
            SiteByYears = new List<SiteByYear>();
        }

    }
}