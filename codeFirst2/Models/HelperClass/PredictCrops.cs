using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace codeFirst2.Models.HelperClass
{
    public class PredictCrops
    {
        private readonly Dictionary<string, Dictionary<string, int>> m_CropsMatrix = new Dictionary<string, Dictionary<string, int>>();

        public void mapExcel()
        {

            //Create COM Objects. Create a COM object for everything that is referenced
            Microsoft.Office.Interop.Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"C:\Users\user\Desktop\טבלה.xlsx");
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            //iterate over the rows and columns and print to the console as it appears in the file
            //excel is not zero based!!
            for (int i = 1; i <= rowCount; i++)
            {
                for (int j = 1; j <= colCount; j++)
                {

                    //write the value to the console
                    if (i == 1 && j >= 2)
                    {
                        Dictionary<string, int> value = new Dictionary<string, int>();
                        for(int k = 1; k <= colCount; k++)
                        {
                            //value.Add(xlRange.Cells[1, k].Value2.ToString(), )
                        }
                        m_CropsMatrix.Add(xlRange.Cells[i, j].Value2, new Dictionary<string, int>());
                    }
                    else if (i > 1 && xlRange.Cells[i, j] != null)
                    {
                        //m_CropsMatrix.Add(xlRange.Cells[i, 1].Value2, )
                    }
                        //write the value to the console
                        if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                        Console.Write(xlRange.Cells[i, j].Value2.ToString() + "\t");
                }
            }

            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //rule of thumb for releasing com objects:
            //  never use two dots, all COM objects must be referenced and released individually
            //  ex: [somthing].[something].[something] is bad

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
        }
    }
}