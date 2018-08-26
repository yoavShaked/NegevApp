using codeFirst2.DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace codeFirst2.DataLayer
{
    public class CropConstrains: ITableEntity
    {
        [Key]
        public int Id { get; set; }
        public int Crop1_Id { get; set; }
        public int Crop2_Id { get; set; }
        public int NumOfYears { get; set; }
    }
}