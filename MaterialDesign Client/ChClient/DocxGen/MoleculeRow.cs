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
        public double? Ratio { get; set; }
        public double nvalue { get; set; }
        //public bool MV { get; set; } //m=true; V=false ???
        public double? mvalue { get; set; } //double? nullozható
        public double? Vvalue { get; set; }
        public double? Denvalue { get; set; }
        public string Mpvalue { get; set; }
        public string Bpvalue { get; set; }

        public void CalculateStartingMaterialValues()
        {


            if (mvalue == null)
            {
                double tmpm = Double.Parse(Vvalue.ToString());
                tmpm = Double.Parse(Vvalue.ToString()) * Double.Parse(Denvalue.ToString());
                nvalue = tmpm / MWvalue;

                nvalue = Math.Round(nvalue, 3);
            }
            else
            {
                nvalue = (double)(mvalue / MWvalue);

                nvalue = Math.Round(nvalue, 3);
            }

        }
        public void CalculateReagentValues(MoleculeRow sm)
        {
            nvalue = sm.nvalue * Double.Parse(Ratio.ToString());

            nvalue = Math.Round(nvalue, 3);

            mvalue = Math.Round(nvalue * MWvalue, 3);


            if (Vvalue.HasValue && Denvalue.HasValue) Vvalue = Math.Round(Double.Parse(mvalue.ToString()) / Double.Parse(Denvalue.ToString()), 3);
            else Vvalue = null;
        }



        public void CalculateProductValues()
        {
            mvalue = Math.Round(nvalue * MWvalue, 3);
        }
    }

}
