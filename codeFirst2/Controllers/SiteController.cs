using codeFirst2.DataLayer;
using codeFirst2.DataLayer.HttpHelper;
using codeFirst2.DataLayer.LightWightesEntities;
using codeFirst2.DataLayer.Repositories;
using codeFirst2.DataLayer.Repositories.NegevEntitiesRepositories;
using codeFirst2.Models.Factory;
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
                IHttpActionResult response = NotFound();
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
                IHttpActionResult response = NotFound();
                ILightWeight siteToSend = m_SiteHendler.GetRowByID(i_SiteID, context);

                if (siteToSend != null)
                {
                    response = Ok(siteToSend);
                }

                return response;
            }
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
                    response = BadRequest("could not post...");
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
                    response = BadRequest("could not update...");
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
                    response = BadRequest("could not delete");
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
