using codeFirst2.DataLayer;
using codeFirst2.DataLayer.HttpHelper;
using codeFirst2.DataLayer.Repositories.NegevEntitiesRepositories;
using codeFirst2.Models.Factory;
using System;
using System.Collections.Generic;
using System.Web.Http;
using codeFirst2.DataLayer.LightWightesEntities;
using codeFirst2.DataLayer.Repositories;


namespace codeFirst2.Controllers
{
    public class CropController : ApiController
    {
        private CropHendler m_CropHendler = new CropHendler(new GenericRepository<Crop>());

        [HttpGet]
        [Route("api/crop/getcrops")]
        public IHttpActionResult GetAllCrops()
        {
            try
            {
                using (EntitiesNegev4 context = new EntitiesNegev4())
                {
                    IHttpActionResult response = NotFound();
                    List<ILightWeight> cropsToSend = m_CropHendler.GetAllTable(context) as List<ILightWeight>;

                    if (cropsToSend.Count > 0)
                    {
                        response = Ok(cropsToSend);
                    }

                    return response;
                }
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("api/crop/getcropbyid")]
        public IHttpActionResult GetCropByID(int id)
        {
            using (EntitiesNegev4 context = new EntitiesNegev4())
            {
                IHttpActionResult response = NotFound();
                ILightWeight cropToSend = m_CropHendler.GetRowByID(id, context);

                if (cropToSend != null)
                {
                    response = Ok(cropToSend);
                }

                return response;
            }
        }

        [HttpPost]
        [Route("api/crop/postcrop")]
        public IHttpActionResult PostNewCrop(Crop i_PostedCrop)
        {
            using (EntitiesNegev4 context = new EntitiesNegev4())
            {
                IHttpActionResult response = Ok();
                bool ok = true;

                try
                {
                    m_CropHendler.PostNewRow(i_PostedCrop, context);
                }
                catch
                {
                    response = BadRequest("could not post...");
                    ok = false;
                }
                if (ok)
                {
                    List<ILightWeight> cropToSend = m_CropHendler.GetAllTable(context) as List<ILightWeight>;
                    response = Ok(cropToSend);
                }

                return response;
            }
        }

        [HttpPut]
        [Route("api/crop/updatecrop")]
        public IHttpActionResult UpdateCrop(Crop i_CropToBeModified)
        {
            using (EntitiesNegev4 context = new EntitiesNegev4())
            {
                IHttpActionResult response = Ok();
                bool ok = true;

                try
                {
                    m_CropHendler.UpdateRow(i_CropToBeModified, context);
                }
                catch
                {
                    response = BadRequest("could not update...");
                    ok = false;
                }
                if (ok)
                {
                    List<ILightWeight> cropsToSend = m_CropHendler.GetAllTable(context) as List<ILightWeight>;
                    response = Ok(cropsToSend);
                }

                return response;
            }
        }

        [HttpDelete]
        [Route("api/crop/deletecrop")]
        public IHttpActionResult DeleteCrop(int i_CropID)
        {
            using (EntitiesNegev4 context = new EntitiesNegev4())
            {
                IHttpActionResult response = Ok();
                bool ok = true;

                try
                {
                    m_CropHendler.DeleteRow(i_CropID, context);
                }
                catch
                {
                    response = BadRequest("could not delete...");
                    ok = false;
                }
                if (ok)
                {
                    List<ILightWeight> cropToSend = m_CropHendler.GetAllTable(context) as List<ILightWeight>;
                    response = Ok(cropToSend);
                }

                return response;
            }
        }
    }
}
