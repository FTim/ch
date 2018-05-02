using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryReader
{
    public class MoleculeData
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string CAS { get; set; }
        public double Mvalue { get; set; }
        public double? mvalue { get; set; }
        public double? Vvalue { get; set; }
        public double? dvalue { get; set; }
        public string mpvalue { get; set; }
        public string bpvalue { get; set; }
        public string purity { get; set; }
    }
}
