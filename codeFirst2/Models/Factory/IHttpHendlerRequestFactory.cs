using codeFirst2.DataLayer;
using codeFirst2.DataLayer.HttpHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace codeFirst2.Models.Factory
{
    public static class IHttpHendlerRequestFactory
    {
        private static eEntityType entityType(string i_EntityName)
        {
            switch (i_EntityName)
            {
                case "Layer": return eEntityType.Layer;
                case "Site": return eEntityType.Site;
                case "Crop": return eEntityType.Crop;
                case "Coordinate": return eEntityType.Coordinate;
                case "SiteByYear": return eEntityType.SiteByYear;
                default: return 0;
            }
        }

        public static IHttpHendlerRequest CreateHttpRequestHendler(string i_EntityName)
        {
            eEntityType type = entityType(i_EntityName);
            IHttpHendlerRequest httpHendlerRequest = null;

            switch (type)
            {
                case eEntityType.Coordinate:
                    {
                        //httpHendlerRequest = new CoordinateHendler();
                        break;
                    }
                case eEntityType.Crop:
                    {
                        //httpHendlerRequest = new CropHendler();
                        break;
                    }
                case eEntityType.Layer:
                    {
                        //httpHendlerRequest = new LayerHendler();
                        break;
                    }
                case eEntityType.Site:
                    {
                        //httpHendlerRequest = new SiteHendler();
                        break;
                    }
                case eEntityType.SiteByYear:
                    {
                        //httpHendlerRequest = new SitesByYearHendler();
                        break;
                    }
            }

            return httpHendlerRequest;
        }
    }
}