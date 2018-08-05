using codeFirst2.DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace codeFirst2.DataLayer
{
    public class Layer: ITableEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public List<SiteByYear> SiteByYears { get; set; }

        public Layer(int i_ID, string i_Name, int i_Year)
        {
            Id = i_ID;
            Name = i_Name;
            Year = i_Year;
        }

        public Layer()
        {
            SiteByYears = new List<SiteByYear>();
        }
    }
}