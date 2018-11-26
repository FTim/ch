using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChDbProject.DTOs
{
    public class StartingMaterialDTO
    {
        public string Name { get; set; }
        public string MoleculeCAS { get; set; }
        public string ReactionName { get; set; }
        public string Location { get; set; }
        public double nValue { get; set; }
        public double? mValue { get; set; }
        public double? VValue { get; set; }

        public StartingMaterial TransformToStartingMaterial()
        {
            StartingMaterial result = new StartingMaterial();

            result.MoleculeCAS = MoleculeCAS;
            result.n = nValue;
            result.m = mValue;
            result.v = VValue;

            

            return result;
        }
    }
}
