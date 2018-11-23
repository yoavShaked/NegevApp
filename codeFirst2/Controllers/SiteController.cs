using codeFirst2.DataLayer;
using codeFirst2.DataLayer.HttpHelper;
using codeFirst2.DataLayer.LightWightesEntities;
using codeFirst2.DataLayer.Repositories;
using codeFirst2.Models.HelperClass;
using System.Collections.Generic;
using System.Web.Http;

namespace codeFirst2.Controllers
{
    public class SiteController : ApiController
    {
        private SiteHendler m_SiteHendler = new SiteHendler(new GenericRepository<Site>());

        [HttpGet]
        [Route("api/site/allsites")]
        public IHttpActionResult GetAllSites()
        {
            using (EntitiesNegev4 context = new EntitiesNegev4())
            {
                IHttpActionResult response = StatusCode(System.Net.HttpStatusCode.ServiceUnavailable);
                List<ILightWeight> sitesToSend = m_SiteHendler.GetAllTable(context) as List<ILightWeight>;

                if (sitesToSend.Count > 0)
                {
                    response = Ok(sitesToSend);
                }

                return response;
            }
        }

        [HttpGet]
        [Route("api/site/sitebyid")]
        public IHttpActionResult GetSite(int i_SiteID)
        {
            using (EntitiesNegev4 context = new EntitiesNegev4())
            {
                IHttpActionResult response = StatusCode(System.Net.HttpStatusCode.ServiceUnavailable);
                ILightWeight siteToSend = m_SiteHendler.GetRowByID(i_SiteID, context);

                if (siteToSend != null)
                {
                    response = Ok(siteToSend);
                }

                return response;
            }
        }

        [HttpGet]
        [Route("api/site/predict")]
        public IHttpActionResult GetPredict(int i_SiteID)
        {
            List<LightWeightCrop> to_return = new List<LightWeightCrop>();
            PredictCrops pc = new PredictCrops();

            List<Crop> crops = pc.predictCropsToGrow(i_SiteID);

            foreach(Crop crop in crops)
            {
                to_return.Add(new LightWeightCrop(crop));
            }

            return Ok(to_return);
        }

        [HttpPost]
        [Route("api/site/postsite")]
        public IHttpActionResult PostSite(Site i_PostedSite)
        {
            using (EntitiesNegev4 context = new EntitiesNegev4())
            {
                IHttpActionResult response = Ok();
                bool ok = true;

                try
                {
                    m_SiteHendler.PostNewRow(i_PostedSite, context);
                }
                catch
                {
                    ok = false;
                    response = StatusCode(System.Net.HttpStatusCode.ServiceUnavailable);
                }
                if (ok)
                {
                    List<ILightWeight> sitesToSend = m_SiteHendler.GetAllTable(context) as List<ILightWeight>;
                    response = Ok(sitesToSend);
                }

                return response;
            }
        }

        [HttpPut]
        [Route("api/site/updatesite")]
        public IHttpActionResult UpdateSite(Site i_ModifideSite)
        {
            using (EntitiesNegev4 context = new EntitiesNegev4())
            {
                IHttpActionResult response = Ok();
                bool ok = true;

                try
                {
                    m_SiteHendler.UpdateRow(i_ModifideSite, context);
                }
                catch
                {
                    response = StatusCode(System.Net.HttpStatusCode.ServiceUnavailable);
                }
                if (ok)
                {
                    List<ILightWeight> sitesToSend = m_SiteHendler.GetAllTable(context) as List<ILightWeight>;
                    response = Ok(sitesToSend);
                }

                return response;
            }
        }

        [HttpDelete]
        [Route("api/site/deletesite")]
        public IHttpActionResult DeleteSite(int i_SiteID)
        {
            using (EntitiesNegev4 context = new EntitiesNegev4())
            {
                IHttpActionResult response = Ok();
                bool ok = true;

                try
                {
                    m_SiteHendler.DeleteRow(i_SiteID, context);
                }
                catch
                {
                    response = StatusCode(System.Net.HttpStatusCode.ServiceUnavailable);
                }
                if (ok)
                {
                    List<ILightWeight> sitesToSend = m_SiteHendler.GetAllTable(context) as List<ILightWeight>;
                    response = Ok(sitesToSend);
                }

                return response;
            }
        }
    }
}
