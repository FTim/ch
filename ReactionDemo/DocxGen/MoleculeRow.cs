using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DocxGen
{
    public class MoleculeRow
    {
        public string Name { get; set; }
        public string CAS { get; set; }
        public double MWvalue { get; set; }
        public double Ratio { get; set; }
        public double nvalue { get; set; }
        //public bool MV { get; set; } //m=true; V=false ???
        public double? mvalue { get; set; } //double? nullozható
        public double? Vvalue { get; set; }
        public double Denvalue { get; set; }
        public double Mpvalue { get; set; }
        public double Bpvalue { get; set; }

        public void CalculateReagentValues()
        {
            //some magic happens here~
        }

        public void CalculateSolventValues()
        {
            //magic~
        }

        public void CalculateProductValues()
        {
            //some other magic happens here~
        }
    }
}
