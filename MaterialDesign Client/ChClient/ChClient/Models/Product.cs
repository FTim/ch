using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChClient.Models
{
    public class Product
    {
        public double MW { get; set; }
        public string MWString { get; set; }
        public double Ratio { get; set; }
        public string RatioString { get; set; }
        public double nValue { get; set; }
        public double mValue { get; set; }

        public List<OutputMessage> Validate()
        {
            List<OutputMessage> result = new List<OutputMessage>();
            
            double tmpmw;
            double tmpr;

            // double.TryParse(VValueString, out tmpv);
            if (String.IsNullOrEmpty(MWString)||String.IsNullOrEmpty(RatioString))
            {
                result.Add(new OutputMessage { Message = "MW value and/or ratio is required!", Level = "error" });
            }

            if (Double.TryParse(MWString, out tmpmw))
            {
                MW = tmpmw;
            }
            else
            {
                result.Add(new OutputMessage { Message = "Cannot convert given MW value to number!", Level = "error" });
            }
            if (Double.TryParse(RatioString, out tmpr))
            {
                Ratio = tmpr;
            }
            else
            {
                result.Add(new OutputMessage { Message = "Cannot convert given ratio to number!", Level = "error" });
                
            }

            return result;
        }
        public void CalculateValues(StartingMaterial sm)
        {
            nValue = Math.Round(Ratio * sm.nValue, 3);
            mValue = nValue * MW;
            MW = Math.Round(MW, 2);
        }
    }
}
