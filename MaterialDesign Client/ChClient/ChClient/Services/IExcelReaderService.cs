using ChClient.Models;
using InventoryReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChClient.Services
{
    public interface IExcelReaderService
    {
        //List<SelectedMolecule> GetMoleculesAsync();

        List<MoleculeData> GetMoleculesFromInventoryFile();
    }
}
