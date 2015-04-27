using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Export
{
    public class ExcelExchange
    {
        Application excelApp;
        public void WriteToFile(String fileName, City from, City to, List<Link> links)
        {
            excelApp = new Application();
            if(excelApp == null){
                Console.WriteLine("Excel couldn't start.");
                return;
            }
            Workbook excelWorkbook = excelApp.Workbooks.Add();
            Worksheet excelWorksheet = excelWorkbook.ActiveSheet;

            // format
            Range excelHeader = excelWorksheet.get_Range("A1", "D1");
            excelHeader.Font.Size = 14;
            excelHeader.Font.Bold = true;
            excelHeader.Cells.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin);

            // add headers
            excelWorksheet.Cells[1,1] = "From";
            excelWorksheet.Cells[1,2] = "To";
            excelWorksheet.Cells[1,3] = "Distance";
            excelWorksheet.Cells[1,4] = "Transport Mode";

            // add data
            int line = 2;
            while (!to.Equals(from))
            {
                foreach (Link link in links)
                {
                    if (from.Equals(link.FromCity))
                    {
                        excelWorksheet.Cells[line, 1] = link.FromCity.Name + " (" + link.FromCity.Country + ")";
                        excelWorksheet.Cells[line, 2] = link.ToCity.Name + " (" + link.ToCity.Country + ")";
                        excelWorksheet.Cells[line, 3] = link.Distance;
                        excelWorksheet.Cells[line, 4] = link.TransportMode;
                        line++;
                        from = link.ToCity;
                    }
                }
            }

            excelApp.DisplayAlerts = false;
            excelWorkbook.SaveAs(fileName);
            excelWorkbook.Close();
            excelApp.Quit();
            return;
        }
    }
}
