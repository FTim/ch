using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChDbProject.DTOs
{
    public class SolventDTO
    {
        public string MoleculeCAS { get; set; }
        public string ReactionName { get; set; }
        public double VValue { get; set; }

        public Solvent TransformToSolvent()
        {
            Solvent result = new Solvent();
            result.MoleculeCAS = MoleculeCAS;
            result.v = VValue;

            return result;
        }
    }
}
