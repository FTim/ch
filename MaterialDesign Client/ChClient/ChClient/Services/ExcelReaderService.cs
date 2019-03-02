using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChClient.Models;
using InventoryReader;

namespace ChClient.Services
{
    public class ExcelReaderService : IExcelReaderService
    {
        
        public List<MoleculeData> GetMoleculesFromInventoryFile()
        {
            InventoryReading reader = new InventoryReading();
            List<MoleculeData> Readed = new List<MoleculeData>();
            try
            {
                reader.FilePath = @"C:\ChClient\Resources\Inventory.xlsx";
                Readed = reader.ReadTo();

            }
            catch (Exception e)
            {
                throw new Exception("Inventory.xlsx not accessible! Close opened Inventory and try again!");
            }

            return Readed;
        }

       /* public List<SelectedMolecule> GetMoleculesAsync()
        {
            List<SelectedMolecule> l = new List<SelectedMolecule>();
            l.AddRange(GetMoleculesFromExcel());

            return l;
        }*/

        /*private List<SelectedMolecule> GetMoleculesFromExcel()
        {
            InventoryReading reader = new InventoryReading();
            try
            {
                reader.FilePath = String.Concat(System.IO.Directory.GetCurrentDirectory(), "\\Resources\\Inventory.xlsx");
                var Readed = reader.ReadTo();

                List<SelectedMolecule> result = new List<SelectedMolecule>();

                foreach (var item in Readed)
                {
                    result.Add(new SelectedMolecule(item.Name, item.CAS, item.Location));
                }
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Inventory.xlsx not accessible! Close opened Inventory and try again!");
            }
        }*/

    }
}
