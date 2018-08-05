using codeFirst2.DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace codeFirst2.DataLayer
{
    public class Crop: ITableEntity
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public List<SiteByYear> SiteByYears { get; set; }

        public Crop(int i_ID, string i_Name, string i_Description, int i_Quantity)
        {
            ID = i_ID;
            Name = i_Name;
            Description = i_Description;
            Quantity = i_Quantity;
        }

        public Crop()
        {
            SiteByYears = new List<SiteByYear>();
        }
    }
}