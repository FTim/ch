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
        public double? mAvailable { get; set; }
        public double? VAvailable { get; set; }
        public double MW { get; set; }
        public double? Den { get; set; }
        public string mpValue { get; set; }
        public string bpValue { get; set; }

       /* public SelectedMolecule(string name, string cas, string location, double? m, double? V, double mw, double? den, string mp, string bp)
        {
            Name = name;
            CAS = cas;
            Location = location;
            mAvailable = m;
            VAvailable = V;
            MW = mw;
            Den = den;
            mpValue = mp;
            bpValue = bp;
        }*/
        public SelectedMolecule()
        {

        }
        public SelectedMolecule(SelectedMolecule selected)
        {
            Name = selected.Name;
            CAS = selected.CAS;
            Location = selected.Location;
            mAvailable = selected.mAvailable;
            VAvailable = selected.VAvailable;
            MW = selected.MW;
            Den = selected.Den;
            mpValue = selected.mpValue;
            bpValue = selected.bpValue;
        }
    }
}
