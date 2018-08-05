using codeFirst2.Models.HelperClass;
using EGIS.ShapeFileLib;
using System;
using System.IO;
using System.Web.Http;


namespace codeFirst2.Controllers
{
    public class DataBaseController : ApiController
    {
        [HttpPost]
        [Route("api/database/PostFile")]
        public IHttpActionResult PostFile()
        {
            try
            {
                ShapeFile shapeFile = null;
                DbfReader dbfFile = null;
                FileMapper fileMapper = null;
                string[] shpFileStr = Directory.GetFiles(@"C:\testFile", "*.shp");
                string[] dbfFilesStr = Directory.GetFiles(@"C:\testFile", "*.dbf");

                if (shpFileStr != null && dbfFilesStr != null)
                {
                    if (shpFileStr.Length == 1 && dbfFilesStr.Length == 1)
                    {
                        shapeFile = new ShapeFile(shpFileStr[0]);
                        dbfFile = new DbfReader(dbfFilesStr[0]);

                        fileMapper = new FileMapper(shapeFile, dbfFile);
                        fileMapper.StartFilter();
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
