using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChDbProject.DTOs
{
    public class ReagentDTO
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string MoleculeCAS { get; set; }
        public string ReactionName { get; set; }
        public double Ratio { get; set; }
        public double? mValue { get; set; }
        public double? VValue { get; set; }

        public string mp { get; set; }
        public string bp { get; set; }
        public double? den { get; set; }

        public Reagent TransformToReagent()
        {
            Reagent result = new Reagent();
            result.MoleculeCAS = MoleculeCAS;
            result.Ratio = Ratio;

            return result;
        }
    }
}
