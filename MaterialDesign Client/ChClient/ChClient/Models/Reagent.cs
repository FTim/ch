using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChClient.Models
{
    public class Reagent :SelectedMolecule
    {
        
        public double Ratio { get; set; }
        public string RatioString { get; set; }
        public double nValue { get; set; }
        public double mValue { get; set; }
        public double VValue { get; set; }


       /* public Reagent(string name, string cas, string location, double? m, double? V, double mw, double? den, string mp, string bp) : base(name, cas, location, m, V, mw, den, mp, bp)
        {

        }*/
        public Reagent()
        {

        }
        public Reagent(SelectedMolecule selected) :base(selected)
        {

        }

        public List<OutputMessage> Validate()
        {
            List<OutputMessage> result = new List<OutputMessage>();

           
            if (String.IsNullOrEmpty(Name) || String.IsNullOrEmpty(CAS) || String.IsNullOrEmpty(Location))
            {
                result.Add(new OutputMessage { Message = "No reagent selected!", Level = "error" });
            }
            double tmpr;
            
            // double.TryParse(VValueString, out tmpv);
            if (String.IsNullOrEmpty(RatioString))
            {
                result.Add(new OutputMessage { Message = "Ratio value is required!", Level = "error" });
                
            }
            
            if (Double.TryParse(RatioString, out tmpr))
            {
                Ratio = tmpr;
            }
            else
            {
                result.Add(new OutputMessage { Message = "Cannot convert given ratio value to number!", Level = "error" });
                
            }

            return result;
        }

        public void CalculateValues(StartingMaterial sm)
        {
            
            nValue = sm.nValue * Ratio;

            nValue = Math.Round(nValue, 3);

            mValue = Math.Round(nValue * MW, 1);


            if (Den.HasValue) VValue = Math.Round(mValue /Den.Value, 3);
            //else VValue = null;
            MW = Math.Round(MW, 2);

            if (mValue > mAvailable)
            {
                //nincs elég anyag
                throw new Exception("Not enough resources!");
                //result.Add(new OutputMessage { Message = "Not enough resources!", Level = "error" });
            }
            if (Den.HasValue)
            {
                if (VValue > VAvailable)
                {
                    throw new Exception("Not enough resources!");
                    //result.Add(new OutputMessage { Message = "Not enough resources!", Level = "error" });
                }
            }
        }
    }
}
