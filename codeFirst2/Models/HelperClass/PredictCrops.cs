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
using codeFirst2.Migrations;

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
                var x = (from y in context.Crops where y.Name.Equals(name) select y).ToList();
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

        private int GetYears(int i_PotentialCropId,int i_GrownCropId)
        {
            using(EntitiesNegev4 context=new EntitiesNegev4())
            {
                var numOfYears =
                    (from table in context.CropConstrains where
                                table.Crop1_Id.Equals(i_PotentialCropId) && table.Crop2_Id.Equals(i_GrownCropId) select table.NumOfYears).ToList();
                return numOfYears[0];
            }
        }

        private bool determineIfCropAllowed(int i_YearsToSeperate, int i_LastYearGrown)
        {
            return DateTime.Now.Year - i_LastYearGrown >= i_YearsToSeperate;
        }
   
        public class CropYearPair
        {
            public List< int> Years { get; set; }
            public Crop _Crop { get;set; }
            public CropYearPair(Crop i_crop)
            {
                Years = new List<int>();
                _Crop = i_crop;
            }
        }


        private List<CropYearPair> GetCropsBySiteId(int i_SiteId)
        {
            using(EntitiesNegev4 context=new EntitiesNegev4())
            {
                Site site;
                SiteRepository siteRepo = new SiteRepository(context);
                site = siteRepo.GetRowByKey(i_SiteId);
                context.Entry(site).Collection(x => x.SiteByYears).Load();
                List<CropYearPair> cropsAndYears = new List<CropYearPair>();
                foreach(SiteByYear sby in site.SiteByYears)
                {
                    context.Entry(sby).Reference(x => x.CurrentCrop).Load();
                    context.Entry(sby).Reference(x => x.CurrentLayer).Load();

                    if (cropsAndYears.Exists(e => e._Crop.ID == sby.CurrentCrop.ID))
                    {
                        cropsAndYears.Find(e => e._Crop.ID == sby.CurrentCrop.ID).Years.Add(sby.CurrentLayer.Year);
                    }
                    else
                    {
                        CropYearPair cyp = new CropYearPair(sby.CurrentCrop);
                        cyp.Years.Add(sby.CurrentLayer.Year);
                        cropsAndYears.Add(cyp); 
                    }
                }

                return cropsAndYears;
            }
        }

        public List<string> predictCropsToGrow(int i_SiteID)
        {
            
            using(EntitiesNegev4 context = new EntitiesNegev4())
            {
                CropRepository cropRepo = new CropRepository(context);
                List<Crop> allCrops = cropRepo.GetNegevEntityCollection() as List<Crop>;
                List<string> namesOfAllowedCrops = new List<string>();
                List<CropYearPair> cropsByYears = GetCropsBySiteId(i_SiteID);
                CropsConstrainsRepository ccr = new CropsConstrainsRepository(context);
                CropConstrains rule = null;

                foreach (Crop crop in allCrops)
                {
                    bool addingOrNot = true;

                    foreach(CropYearPair cyp in cropsByYears)
                    {
                        int growingCropID = getCropIdByName(cyp._Crop.Name);
                       
                        if (rule != null)
                        {
                            int seprateNumOfYears = GetYears(crop.ID, growingCropID);
                            if (!determineIfCropAllowed(seprateNumOfYears, cyp.Years[cyp.Years.Count - 1]))
                            {
                                addingOrNot = false;
                                break;
                            }
                        }
                    }

                    if (addingOrNot)
                    {
                        namesOfAllowedCrops.Add(crop.Name);
                    }
                }

                return namesOfAllowedCrops;
            }
        }

    }
}