using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChClient.Models
{
    public class Solvent:SelectedMolecule
    {
       
        public double VValue { get; set; }
        public string VValueString { get; set; }

        /*public Solvent(string name, string cas, string location, double? m, double? V, double mw, double? den, string mp, string bp) : base(name, cas, location, m, V, mw, den, mp, bp)
        {

        }*/
        public Solvent()
        {

        }
        public Solvent(SelectedMolecule selected) :base(selected)
        {

        }
        public List<OutputMessage> Validate()
        {
            List<OutputMessage> result = new List<OutputMessage>();
            if (String.IsNullOrEmpty(Name) || String.IsNullOrEmpty(CAS) || String.IsNullOrEmpty(Location))
            {
                result.Add(new OutputMessage { Message = "No solvent selected!", Level = "error" });
                
            }
            double tmpv;

            // double.TryParse(VValueString, out tmpv);
            if (String.IsNullOrEmpty(VValueString))
            {
                result.Add(new OutputMessage { Message = "V value is required!", Level = "error" });
                
            }

            if (Double.TryParse(VValueString, out tmpv))
            {
                VValue = tmpv;
                if (VValue > VAvailable)
                {
                    result.Add(new OutputMessage { Message = "Not enough resources!", Level = "error" });

                }
            }
            else
            {
                result.Add(new OutputMessage { Message = "Cannot convert given V value to number!", Level = "error" });
                
            }

            return result;
        }

    }
}
