using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChClient.Models
{
    public class StartingMaterial :SelectedMolecule
    {
        
        public string mValueString { get; set; }
        public double mValue { get; set; }
        public string VValueString { get; set; }
        public double VValue { get; set; }
        public double nValue { get; set; }

        /*public StartingMaterial(string name, string cas, string location, double? m, double? V, double mw, double? den, string mp, string bp) : base(name, cas, location, m, V, mw, den, mp, bp)
        {

        }*/
        public StartingMaterial(SelectedMolecule selected) :base(selected)
        {

        }
        public StartingMaterial()
        {

        }

        public List<OutputMessage> Validate()
        {
            List<OutputMessage> result = new List<OutputMessage>();
            
            if (String.IsNullOrEmpty(Name) || String.IsNullOrEmpty(CAS) || String.IsNullOrEmpty(Location))
            {
                result.Add(new OutputMessage { Message = "No starting material selected!", Level = "error" });
                //result.NoStartingMaterialSelectedError = "No starting material selected!";
            }
            double tmpm;
            // Double.TryParse(mValueString, out tmpm);
            double tmpv;
            // double.TryParse(VValueString, out tmpv);
            if (String.IsNullOrEmpty(mValueString) && String.IsNullOrEmpty(VValueString))
            {
                result.Add(new OutputMessage { Message = "Neither m or V value given! Either of them is required!", Level = "error" });
                //result.HasBothValuesError = "Neither m or V value given! Either of them is required!";
            }
            if ((!String.IsNullOrEmpty(mValueString)) && (!String.IsNullOrEmpty(VValueString)))
            {
                result.Add(new OutputMessage { Message = "Both m and V values given! Only one of them is required!", Level = "error" });
                //result.HasBothValuesError = "Both m and V values given! Only one of them is required!";
            }

            if (Double.TryParse(mValueString, out tmpm))
            {
                mValue = tmpm;
                if (mValue > mAvailable) result.Add(new OutputMessage { Message = "Not enough resources!", Level = "error" });
            }
            else
            {
                if (Double.TryParse(VValueString, out tmpv))
                {
                    VValue = tmpv;
                    if (VValue > VAvailable) result.Add(new OutputMessage { Message = "Not enough resources!", Level = "error" });
                }
                else
                {
                    result.Add(new OutputMessage { Message = "Cannot convert given m/V value to number!", Level = "error" });
                    //result.CannotConvertNumberError = "Cannot convert given m/V value to number!";
                }
            }

            return result;
        }

        public void CalculateValues()
        {
            if (!string.IsNullOrEmpty(mValueString))
            {

                nValue = mValue / MW;

                nValue = Math.Round(nValue, 3);
                mValue = Math.Round(mValue, 1);
            }
            else
            {
                mValue = VValue * Den.Value;

                nValue = mValue / MW;

                nValue = Math.Round(nValue, 3);
                mValue = Math.Round(mValue, 1);
                VValue = Math.Round(VValue, 1);

                /*
                double tmpm = Double.Parse(Vvalue.ToString());
                tmpm = Double.Parse(Vvalue.ToString()) * Double.Parse(Denvalue.ToString());
                nvalue = tmpm / MWvalue;

                nvalue = Math.Round(nvalue, 3);*/
            }
            MW = Math.Round(MW, 2);
        }
    }
}
