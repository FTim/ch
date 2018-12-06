using ChClient.Models;
using InventoryGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChClient.Services
{
    public class ExcelWriterService : IExcelWriterService
    {
        public void ExportExcelAsync(string filename, List<SelectedMolecule> molecules)
        {
            InventoryGenerating generator = new InventoryGenerating();
            List<MoleculeData> moleculedatas = new List<MoleculeData>();

            foreach (var item in molecules)
            {
                moleculedatas.Add(new MoleculeData() { Name = item.Name, CAS = item.CAS, Location = item.Location, Mvalue = item.MW, mvalue = item.mAvailable, Vvalue = item.VAvailable, dvalue = item.Den, bpvalue = item.bpValue, mpvalue = item.mpValue, purity = item.Purity });
            }

            generator.Generate(filename, moleculedatas);
        }
    }
}
