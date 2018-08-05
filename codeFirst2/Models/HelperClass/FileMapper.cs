using codeFirst2.DataLayer;
using codeFirst2.DataLayer.Repositories.NegevEntitiesRepositories;
using EGIS.ShapeFileLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Drawing;
using System.Linq;

namespace codeFirst2.Models.HelperClass
{
    public class FileMapper
    {
        private readonly ShapeFile r_ShapeFile;
        private readonly DbfReader r_DbfReader;
        private readonly List<string> r_SitesNames = new List<string>();
        private readonly List<string> r_Regions = new List<string>();
        private readonly List<string> r_Dunams = new List<string>();
        private readonly Dictionary<string, string> r_CropMapper = new Dictionary<string, string>();
        private int m_CoordinateID = 0;
        private int m_SiteByYearID = 0;
        private int m_SiteID = 0;
        private int m_CropId = 0;
        private int m_LayersId = 0;
        private SiteByYearRepository m_siteByYearRepository = null;
        private SiteRepository m_SiteRepository = null;
        private CropRepository m_CropRepository = null;
        private CoordinateRepository m_CoordinateReository = null;
        private LayerRepository m_LayerRepository = null;

        public FileMapper(ShapeFile i_ShapeFile, DbfReader i_DbfReader)
        {
            r_ShapeFile = i_ShapeFile;
            r_DbfReader = i_DbfReader;
            r_SitesNames = r_DbfReader.GetRecords(i_DbfReader.IndexOfFieldName("Hel_Name")).ToList();
            r_Regions = r_DbfReader.GetRecords(i_DbfReader.IndexOfFieldName("Ezor")).ToList();
            r_Dunams = r_DbfReader.GetRecords(i_DbfReader.IndexOfFieldName("Dunam")).ToList();
        }

        private void initializeRepositories(DbContext i_Context)
        {
            m_CoordinateReository = new CoordinateRepository(i_Context);
            m_CropRepository = new CropRepository(i_Context);
            m_LayerRepository = new LayerRepository(i_Context);
            m_SiteRepository = new SiteRepository(i_Context);
            m_siteByYearRepository = new SiteByYearRepository(i_Context);
        }

        private bool isSitesExsits(Site i_Site)
        {
            bool answer = false;

            List<Site> sites = m_SiteRepository.GetNegevEntityCollection().ToList();

            foreach(Site site in sites)
            {
                if(site.Id == i_Site.Id)
                {
                    answer = true;
                    break;
                }
            }

            return answer;
        }

        public void StartFilter()
        {
            int i;
            int key;
            int objecyID_Index = r_DbfReader.IndexOfFieldName("OBJECTID");
            int site_Name_Index = r_DbfReader.IndexOfFieldName("Hel_Name");
            int region_Name_Index = r_DbfReader.IndexOfFieldName("Ezor");
            int site_Dunam_Index = r_DbfReader.IndexOfFieldName("Dunam");
            Site current_site;
            string current_crop_name;
            Coordinate current_coordinate;
            SiteByYear current_site_by_year;
            Crop current_crop;
            string current_sug;
            Dictionary<int, Layer> layers_dicitonary = new Dictionary<int, Layer>();
            Dictionary<string, Crop> crops_dictionary = new Dictionary<string, Crop>();
            Dictionary<int, Dictionary<string, int>> site_by_year_indexes = new Dictionary<int, Dictionary<string, int>>();

            using (EntitiesNegev4 context = new EntitiesNegev4())
            {
                context.Configuration.AutoDetectChangesEnabled = false;

                initializeRepositories(context);

                for (i = 13; i < 18; i++)
                {
                    key = 2000 + i;

                    layers_dicitonary.Add(key, new Layer()
                    {
                        Id = m_LayersId++,
                        Name = key.ToString(),
                        Year = key,
                        SiteByYears = new List<SiteByYear>()
                    });

                    site_by_year_indexes.Add(key, new Dictionary<string, int>());

                    site_by_year_indexes[key].Add("Gidul1", r_DbfReader.IndexOfFieldName("Gidul_I_" + i.ToString()));
                    site_by_year_indexes[key].Add("Gidul2", r_DbfReader.IndexOfFieldName("Gidul_II_" + (17 - i).ToString()));
                    site_by_year_indexes[key].Add("Sug1", r_DbfReader.IndexOfFieldName("Sug_I_" + i.ToString()));
                    site_by_year_indexes[key].Add("Sug2", r_DbfReader.IndexOfFieldName("Sug_II_" + i.ToString()));
                    site_by_year_indexes[key].Add("Details", r_DbfReader.IndexOfFieldName("Details_" + i.ToString()));

                }

                for (int recorde_index = 0; recorde_index < r_DbfReader.GetRecords(objecyID_Index).Length; recorde_index++, m_SiteID++)
                {
                    current_site = new Site()
                    {
                        Dunam = int.Parse(r_DbfReader.GetField(recorde_index, site_Dunam_Index)),
                        Region = r_DbfReader.GetField(recorde_index, region_Name_Index).Trim(),
                        Name = r_DbfReader.GetField(recorde_index, site_Name_Index).Trim(),
                        Id = m_SiteID,
                        Coordinates = new List<Coordinate>(),
                        SiteByYears = new List<SiteByYear>()
                    };

                    //adding

                     m_SiteRepository.AddRow(current_site);

                    for (i = 0; i < getShapeCoordinates(recorde_index)[0].Length; i++)
                    {
                        current_coordinate = new Coordinate()
                        {
                            ID = m_CoordinateID,
                            X = getShapeCoordinates(recorde_index)[0][i].X,
                            Y = getShapeCoordinates(recorde_index)[0][i].Y,
                            Site = current_site
                        };

                        if (!isCoordinateExistsInSite(current_coordinate, current_site))
                        {
                            //adding
                            current_site.Coordinates.Add(current_coordinate);
                            m_CoordinateReository.AddRow(current_coordinate);
                            m_CoordinateID++;
                        }
                    }

                    for (int year = 2013; year < 2018; year++)
                    {
                        for (int j = 1; j <= 2; j++)
                        {
                            try
                            {
                                current_crop_name = r_DbfReader.GetField(recorde_index, site_by_year_indexes[year]["Gidul" + j.ToString()]).Trim();
                            }
                            catch (Exception ex)
                            {
                                current_crop_name = null;
                            }
                            if (!string.IsNullOrWhiteSpace(current_crop_name))
                            {
                                try
                                {
                                    current_sug = r_DbfReader.GetField(recorde_index, site_by_year_indexes[year]["Sug" + j.ToString()]).Trim();
                                }
                                catch (Exception ex)
                                {
                                    current_sug = null;
                                }
                                if (!string.IsNullOrWhiteSpace(current_sug))
                                {
                                    current_crop_name += " " + current_sug;
                                }

                                if (!crops_dictionary.ContainsKey(current_crop_name))
                                {
                                    current_crop = new Crop()
                                    {
                                        ID = m_CropId++,
                                        Name = current_crop_name,
                                        Description = "",
                                        Quantity = 0,
                                        SiteByYears = new List<SiteByYear>()
                                    };
                                    crops_dictionary.Add(current_crop_name, current_crop);
                                }
                                else
                                {
                                    current_crop = crops_dictionary[current_crop_name];
                                }

                                current_site_by_year = new SiteByYear()
                                {
                                    Id = m_SiteByYearID++,
                                    CurrentCrop = current_crop,
                                    CurrentSite = current_site,
                                    CurrentLayer = layers_dicitonary[year]
                                };

                                current_crop.SiteByYears.Add(current_site_by_year);
                                current_site.SiteByYears.Add(current_site_by_year);
                                layers_dicitonary[year].SiteByYears.Add(current_site_by_year);

                                //adding
                                m_siteByYearRepository.AddRow(current_site_by_year);
                            }
                        }
                    }
                }

                foreach (KeyValuePair<string, Crop> crop in crops_dictionary)
                {
                    //adding
                    m_CropRepository.AddRow(crop.Value);
                }

                foreach (KeyValuePair<int, Layer> layer in layers_dicitonary)
                {
                    //adding
                    m_LayerRepository.AddRow(layer.Value);
                }

                context.Configuration.AutoDetectChangesEnabled = true;
                m_CoordinateReository.Save();
            }
        }

        private bool isCoordinateExistsInSite(Coordinate i_CurrentCoordinate, Site i_CurrentSite)
        {
            bool answer = false;

            foreach (Coordinate coordiante in i_CurrentSite.Coordinates)
            {
                if (coordiante.Y == i_CurrentCoordinate.Y && coordiante.X == i_CurrentCoordinate.X)
                {
                    answer = true;
                    break;
                }
            }

            return answer;
        }

        private ReadOnlyCollection<PointF[]> getShapeCoordinates(int i_RecordeIndex)
        {
            return r_ShapeFile.GetShapeData(i_RecordeIndex);
        }

    }
}