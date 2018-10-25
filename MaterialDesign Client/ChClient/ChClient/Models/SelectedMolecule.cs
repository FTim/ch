using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChClient.Models
{
    public class SelectedMolecule
    {
        public string Name { get; set; }
        public string CAS { get; set; }
        public string Location { get; set; }

        public SelectedMolecule(string name, string cas, string location)
        {
            Name = name;
            CAS = cas;
            Location = location;
        }
    }
}
