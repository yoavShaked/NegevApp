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
        bool is_not_exist_rull_allowed = true;

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
                CropRepository cropsRepo = new CropRepository(context);
                List<Crop> cropsDataBase = cropsRepo.GetNegevEntityCollection() as List<Crop>;
                bool answer = false;

                foreach (Crop crop in cropsDataBase)
                {
                    if (crop.Name.Equals(name))
                    {
                        answer = true;
                        break;
                    }
                }

                return answer;
            }
        }

        private int[] convertNamesToIds(string[] names)
        {
            int[] ids = new int[names.Length];
            for (int i = 1; i < names.Length; i++)
            {
                ids[i] = getCropIdByName(names[i]);
            }
            return ids;
        }

        private void addAllMissingCrops(string[] names)
        {
            for (int i = 1; i < names.Length; i++)
            {
                if (isCropExist(names[i]) == false)
                {
                    addCrop(names[i]);
                }
            }
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
            int[] ids = convertNamesToIds(data[0]);



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

        public int GetYears(int i_PotentialCropId, int i_GrownCropId)
        {
            using (EntitiesNegev4 context = new EntitiesNegev4())
            {
                var numOfYears =
                    (from x in context.CropConstrains
                     where x.Crop1_Id.Equals(i_PotentialCropId) && x.Crop2_Id.Equals(i_GrownCropId)
                     select x.NumOfYears).ToList();

                return numOfYears.Count == 0 ? (is_not_exist_rull_allowed ? -1 : 100000) : numOfYears[0];
            }
        }

        private bool determineIfCropAllowed(int i_YearsToSeperate, int i_LastYearGrown)
        {
            return DateTime.Now.Year - i_LastYearGrown >= i_YearsToSeperate;
        }

        public class CropYearPair
        {
            public int Year { get; set; }
            public Crop _Crop { get; set; }
            public CropYearPair(Crop i_crop, int i_Year)
            {
                Year = i_Year;
                _Crop = i_crop;
            }
        }

        private List<CropYearPair> GetCropsBySiteId(int i_SiteId)
        {
            using (EntitiesNegev4 context = new EntitiesNegev4())
            {
                Site site;
                SiteRepository siteRepo = new SiteRepository(context);
                site = siteRepo.GetRowByKey(i_SiteId);
                context.Entry(site).Collection(x => x.SiteByYears).Load();
                List<CropYearPair> cropsAndYears = new List<CropYearPair>();

                foreach (SiteByYear sby in site.SiteByYears)
                {
                    context.Entry(sby).Reference(x => x.CurrentCrop).Load();
                    context.Entry(sby).Reference(x => x.CurrentLayer).Load();
                    cropsAndYears.Add(new CropYearPair(sby.CurrentCrop, sby.CurrentLayer.Year));
                }

                return cropsAndYears;
            }
        }

        public List<Crop> predictCropsToGrow(int i_SiteID)
        {
            List<Crop> allAllowedCrops = new List<Crop>();

            using (EntitiesNegev4 context = new EntitiesNegev4())
            {
                CropRepository cropRepo = new CropRepository(context);
                List<Crop> allCrops = cropRepo.GetNegevEntityCollection() as List<Crop>;
                List<CropYearPair> cropYearsPairs = GetCropsBySiteId(i_SiteID);
                CropsConstrainsRepository cropConstrainRepo = new CropsConstrainsRepository(context);

                foreach (Crop crop in allCrops)
                {
                    bool allowGrow = true;

                    foreach (CropYearPair cyp in cropYearsPairs)
                    {
                        int allowedYears = GetYears(crop.ID, cyp._Crop.ID);
                        if (!determineIfCropAllowed(allowedYears, cyp.Year))
                        {
                            allowGrow = false;
                            break;
                        }
                    }

                    if(allowGrow)
                    {
                        allAllowedCrops.Add(crop);
                    }
                }

            }

            return allAllowedCrops;
        }
    }
}