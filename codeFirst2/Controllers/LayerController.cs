using codeFirst2.DataLayer;
using codeFirst2.DataLayer.HttpHelper;
using codeFirst2.DataLayer.LightWightesEntities;
using codeFirst2.DataLayer.Repositories;
using codeFirst2.DataLayer.Repositories.NegevEntitiesRepositories;
using codeFirst2.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace codeFirst2.Controllers
{
    public class LayerController : ApiController
    {
        private LayerHendler m_LayerHendler = new LayerHendler(new GenericRepository<Layer>());

        [HttpGet]
        [Route("api/layer/getlayers")]
        public IHttpActionResult GetAllLayers()
        {
            using (EntitiesNegev4 context = new EntitiesNegev4())
            {
                IHttpActionResult response = NotFound();
                List<ILightWeight> layersToSend = m_LayerHendler.GetAllTable(context) as List<ILightWeight>;

                if (layersToSend.Count > 0)
                {
                    response = Ok(layersToSend);
                }

                return response;
            }
        }

        [HttpGet]
        [Route("api/layer/layersNamesIDs")]
        public IHttpActionResult GetLayersNamesAndIDS()
        {
            using (EntitiesNegev4 context = new EntitiesNegev4())
            {
                IHttpActionResult response = NotFound();
                List<ILightWeight> layers = m_LayerHendler.GetAllTable(context) as List<ILightWeight>;
                List<LayerIdentity> layersToSend = null;

                if (layers.Count > 0)
                {
                    layersToSend = new List<LayerIdentity>();

                    foreach (ILightWeight layer in layers)
                    {
                        layersToSend.Add(new LayerIdentity()
                        {
                            ID = (layer as LightWeightLayer).ID,
                            Name = (layer as LightWeightLayer).Name
                        });
                    }

                    response = Ok(layersToSend);
                }

                return response;
            }
        }

        [HttpGet]
        [Route("api/layer/layerbyid")]
        public IHttpActionResult GetLayer(int i_LayerID)
        {
            using (EntitiesNegev4 context = new EntitiesNegev4())
            {
                IHttpActionResult response = NotFound();
                ILightWeight layerToSend = m_LayerHendler.GetRowByID(i_LayerID, context);

                if (layerToSend != null)
                {
                    response = Ok(layerToSend);
                }

                return response;
            }
        }

        [HttpPost]
        [Route("api/layer/postlayer")]
        public IHttpActionResult PostLayer(Layer i_PostedLayer)
        {
            using (EntitiesNegev4 context = new EntitiesNegev4())
            {
                IHttpActionResult response = Ok();
                bool ok = true;

                try
                {
                    m_LayerHendler.PostNewRow(i_PostedLayer, context);
                }
                catch
                {
                    response = BadRequest("could not post...");
                    ok = false;
                }
                if (ok)
                {
                    List<ILightWeight> layersToSend = m_LayerHendler.GetAllTable(context) as List<ILightWeight>;
                    response = Ok(layersToSend);
                }

                return response;
            }
        }

        [HttpPut]
        [Route("api/layer/updatelayer")]
        public IHttpActionResult UpdateLayer(Layer i_ModifideLayer)
        {
            using (EntitiesNegev4 context = new EntitiesNegev4())
            {
                IHttpActionResult response = Ok();
                bool ok = true;

                try
                {
                    m_LayerHendler.UpdateRow(i_ModifideLayer, context);
                }
                catch
                {
                    response = BadRequest("could not update...");
                    ok = false;
                }
                if (ok)
                {
                    List<ILightWeight> layersToSend = m_LayerHendler.GetAllTable(context) as List<ILightWeight>;
                    response = Ok(layersToSend);
                }

                return response;
            }
        }

        [HttpDelete]
        [Route("api/layer/deletelayer")]
        public IHttpActionResult DeleteLayer(int i_LayerID)
        {
            using (EntitiesNegev4 context = new EntitiesNegev4())
            {
                IHttpActionResult response = Ok();
                bool ok = true;

                try
                {
                    m_LayerHendler.DeleteRow(i_LayerID, context);
                }
                catch
                {
                    response = BadRequest("could not delete");
                }
                if (ok)
                {
                    List<ILightWeight> layersToSend = m_LayerHendler.GetAllTable(context) as List<ILightWeight>;
                    response = Ok(layersToSend);
                }

                return response;
            }
        }
    }
}
