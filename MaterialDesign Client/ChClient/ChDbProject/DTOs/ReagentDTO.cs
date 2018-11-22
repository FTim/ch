using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChDbProject.DTOs
{
    public class ReagentDTO
    {
        public string MoleculeCAS { get; set; }
        public string ReactionName { get; set; }
        public double Ratio { get; set; }

        public Reagent TransformToReagent()
        {
            Reagent result = new Reagent();
            result.MoleculeCAS = MoleculeCAS;
            result.Ratio = Ratio;

            return result;
        }
    }
}
