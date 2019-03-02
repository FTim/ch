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
        public string Amount { get; set; }
        public double MW { get; set; }
        public string MWString { get; set; }
        public double? Den { get; set; }
        public string DenString { get; set; }
        public string mpValue { get; set; }
        public string bpValue { get; set; }
        public string Purity { get; set; }

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

        public List<OutputMessage> Validate(string type)
        {
            List<OutputMessage> result = new List<OutputMessage>();

            if (string.IsNullOrEmpty(Name))
            {
                result.Add(new OutputMessage() { Message = "Molecule name required!", Level="error" });
            }
            if (string.IsNullOrEmpty(CAS))
            {
                result.Add(new OutputMessage() { Message = "Molecule CAS required!", Level = "error" });
            }
            if (string.IsNullOrEmpty(Location))
            {
                result.Add(new OutputMessage() { Message = "Molecule location required!", Level = "error" });
            }
            if (string.IsNullOrEmpty(MWString))
            {
                result.Add(new OutputMessage() { Message = "Molecule M required!", Level = "error" });
            }
            else
            {
                double tmpM;
                if (Double.TryParse(MWString, out tmpM))
                {
                    MW = tmpM;
                }
                else
                {
                    result.Add(new OutputMessage { Message = "Cannot convert given M value to number!", Level = "error" });
                }
            }

            if (!string.IsNullOrEmpty(DenString))
            {
                double tmpDen;
                if (Double.TryParse(DenString, out tmpDen))
                {
                    Den = tmpDen;
                }
                else
                {
                    result.Add(new OutputMessage { Message = "Cannot convert given density value to number!", Level = "error" });
                }
            }
            else
            {
                result.Add(new OutputMessage { Message = "Molecule density will be empty", Level = "info" });
            }

            if (string.IsNullOrEmpty(Amount))
            {
                result.Add(new OutputMessage() { Message = "Amount required!", Level = "error" });
            }
            else
            {
                double tmpA;
                if (Double.TryParse(Amount, out tmpA))
                {
                    if(type== "m (g)")
                    {
                        mAvailable = tmpA;
                    }
                    else
                    {
                        if (type == "V (ml)")
                        {
                            VAvailable = tmpA;
                        }
                        else
                        {
                            result.Add(new OutputMessage { Message = "Amoutn type required!", Level = "error" });
                        }
                            
                    }
                }
                else
                {
                    result.Add(new OutputMessage { Message = "Cannot convert given amount to number!", Level = "error" });
                }
            }

            if(string.IsNullOrEmpty(mpValue)) result.Add(new OutputMessage { Message = "Molecule mp will be empty", Level = "info" });
            if (string.IsNullOrEmpty(bpValue)) result.Add(new OutputMessage { Message = "Molecule bp will be empty", Level = "info" });
            if (string.IsNullOrEmpty(Purity)) result.Add(new OutputMessage { Message = "Molecule purity will be empty", Level = "info" });



            return result;
        }
    }
}
