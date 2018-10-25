using ChDbProject;
using InventoryReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             Startup project-hez is hozzáadni az EF-et, különben hibát dob!!!
             */

            InventoryReading inventoryReader = new InventoryReading();
            inventoryReader.FilePath = @"C:\Users\Tim\Desktop\Inventory.xlsx";
            List<MoleculeData> Allreaded = new List<MoleculeData>();
            Allreaded = inventoryReader.ReadTo();

            List<string> locationsonly = new List<string>();

            foreach (var item in Allreaded)
            {
                locationsonly.Add(item.Location);
            }

            locationsonly = locationsonly.Distinct().ToList();

            foreach (var item in locationsonly)
            {
                DbAccess.AddLocation(item);
            }

            Console.WriteLine("locations - done");

            foreach (var item in Allreaded)
            {
                //TODO:típusok
                DbAccess.AddMoleculeStatic(item.Name, item.CAS, item.Mvalue, item.dvalue, item.mpvalue, item.bpvalue, item.purity);
            }

        Console.ReadKey();
        }
    }
}
