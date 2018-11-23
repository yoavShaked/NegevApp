using codeFirst2.DataLayer.Repositories;
using System.ComponentModel.DataAnnotations;

namespace codeFirst2.DataLayer
{
    public class SiteByYear: ITableEntity
    {
        [Key]
        public int Id { get; set; }
        public Layer CurrentLayer { get; set; }
        public Crop CurrentCrop { get; set; }
        public Site CurrentSite { get; set; }

        public SiteByYear(int i_ID, Layer i_Layer, Crop i_Crop, Site i_Site)
        {
            Id = i_ID;
            CurrentCrop = i_Crop;
            CurrentLayer = i_Layer;
            CurrentSite = i_Site;
        }

        public SiteByYear()
        {

        }
    }
}