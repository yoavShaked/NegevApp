using codeFirst2.DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace codeFirst2.DataLayer.LightWightesEntities
{
    public class LightWeightCrop: ILightWeight
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }

        public LightWeightCrop(ITableEntity te)
        {
            Crop i_Crop = (Crop)te;
            ID = i_Crop.ID;
            Name = i_Crop.Name;
            Description = i_Crop.Description;
            Quantity = i_Crop.Quantity;
        }
    }
}