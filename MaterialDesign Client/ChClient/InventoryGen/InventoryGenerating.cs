using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryGen
{
    public class InventoryGenerating
    {
        public void Generate(string filename, List<MoleculeData> molecules)
        {
            using(ExcelPackage package = new ExcelPackage(new FileInfo(filename))){
                //var ws=package.Workbook.Worksheets.Contains(ExcelWorksheet.)
                var ws = package.Workbook.Worksheets.Add("InventoryExported");

                //header cells+fix them
                ws.Cells["B1"].Value = "Name";
                ws.Cells["C1"].Value = "Location";
                ws.Cells["D1"].Value = "CAS	";
                ws.Cells["E1"].Value = "M (g/mol)";
                ws.Cells["F1"].Value = "m (g)";
                ws.Cells["G1"].Value = "V (ml)";
                ws.Cells["H1"].Value = "d (g/ml)";
                ws.Cells["I1"].Value = "mp (°C)";
                ws.Cells["J1"].Value = "bp (°C)";
                ws.Cells["K1"].Value = "Purity (%)";
                ws.View.FreezePanes(2, 1);


                for (int row = 2; row < molecules.Count+2; row++)
                {
                    ws.SetValue(row, 1, row-1);
                    ws.SetValue(row, 2, molecules.ElementAt(row - 2).Name);
                    ws.SetValue(row, 3, molecules.ElementAt(row - 2).Location);
                    ws.SetValue(row, 4, molecules.ElementAt(row - 2).CAS);
                    ws.SetValue(row, 5, molecules.ElementAt(row - 2).Mvalue);
                    if (molecules.ElementAt(row - 2).mvalue.HasValue) ws.SetValue(row, 6, molecules.ElementAt(row - 2).mvalue);
                    if (molecules.ElementAt(row - 2).Vvalue.HasValue) ws.SetValue(row, 7, molecules.ElementAt(row - 2).Vvalue);
                    if (molecules.ElementAt(row - 2).dvalue.HasValue) ws.SetValue(row, 8, molecules.ElementAt(row - 2).dvalue);
                    ws.SetValue(row, 9, molecules.ElementAt(row - 2).mpvalue);
                    ws.SetValue(row, 10, molecules.ElementAt(row - 2).bpvalue);
                    ws.SetValue(row, 11, molecules.ElementAt(row - 2).purity);
                }

                package.Save();
            }
            
        }
    }
}
