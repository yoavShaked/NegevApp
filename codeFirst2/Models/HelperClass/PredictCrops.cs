using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.IO;
using codeFirst2.DataLayer;
using codeFirst2.DataLayer.Repositories;
using codeFirst2.DataLayer.HttpHelper;
using codeFirst2.DataLayer.LightWightesEntities;
using codeFirst2.DataLayer.Repositories.NegevEntitiesRepositories;

namespace codeFirst2.Models.HelperClass
{
    public class PredictCrops
    {

        private void addCrop(string name)
        {
            using (EntitiesNegev4 context = new EntitiesNegev4())
            {
                CropRepository crops = new CropRepository(context);
                List<Crop> cropsDataBase = crops.GetNegevEntityCollection() as List<Crop>;
                CropsConstrainsRepository ccrs = new CropsConstrainsRepository(context);

                Crop c = new Crop();

                c.Name = name;
                c.Quantity = 0;
                c.SiteByYears = null;
                c.Description = "";
                crops.AddRow(c);
                crops.Save();

            }
        }
        private bool isCropExist(string name)
        {
            using (EntitiesNegev4 context = new EntitiesNegev4())
            {
                CropRepository crops = new CropRepository(context);
                List<Crop> cropsDataBase = crops.GetNegevEntityCollection() as List<Crop>;
                CropsConstrainsRepository ccrs = new CropsConstrainsRepository(context);
                foreach (Crop crop in cropsDataBase)
                {
                    if (crop.Name.Equals(name))
                    {
                        return true;
                    }
                }

            }

            return false;
        }
        private int [] convertNamesToIds(string [] names)
        {
            int[] ids = new int[names.Length] ;
            for(int i=1;i<names.Length;i++)
            {
                ids[i] = getCropIdByName(names[i]);
            }
            return ids;
        }
        private void addAllMissingCrops(string[] names)
        {
            for (int i = 1; i < names.Length; i++)
                if (!isCropExist(names[i]))
                    addCrop(names[i]);
        }
 
        int getCropIdByName(string name)
        {
            using (EntitiesNegev4 context = new EntitiesNegev4())
            {
                CropRepository crops = new CropRepository(context);
                List<Crop> cropsDataBase = crops.GetNegevEntityCollection() as List<Crop>;
                CropsConstrainsRepository ccrs = new CropsConstrainsRepository(context);
                List<Crop> x = (from y in context.Crops where y.Name.Equals(name) select y).ToList<Crop>();
                return x[0].ID;
            }
        }
        public void mapExcel()
        {
            CropHendler m_CropHendler = new CropHendler(new GenericRepository<Crop>());
            var filePath = @"C:\Users\user\Desktop\excel\rawData.csv";
            var data = File.ReadLines(filePath).Select(x => x.Split(',')).ToArray();

            addAllMissingCrops(data[0]);
            int currentId;
            int [] ids = convertNamesToIds(data[0]);



            using (EntitiesNegev4 context = new EntitiesNegev4())
            {
                CropsConstrainsRepository cropsContains = new CropsConstrainsRepository(context);

                for (int i = 1; i < data.Length; i++)
                {
                    currentId = getCropIdByName(data[i][0]);

                    for (int j = 1; j < data[i].Length; j++)
                    {
                        CropConstrains cc = new CropConstrains();
                        cc.Crop1_Id = currentId;
                        cc.Crop2_Id = ids[j];
                        cc.NumOfYears = int.Parse(data[i][j]);

                        cropsContains.AddRow(cc);
                        cropsContains.Save();
                    }
                }
            }

        }

    }
}