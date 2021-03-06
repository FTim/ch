﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChDbProject.DTOs
{
    public class SolventDTO
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string MoleculeCAS { get; set; }
        public string ReactionName { get; set; }
        public double VValue { get; set; }

        public string mp { get; set; }
        public string bp { get; set; }
        public Solvent TransformToSolvent()
        {
            Solvent result = new Solvent();
            result.MoleculeCAS = MoleculeCAS;
            result.v = VValue;

            return result;
        }
    }
}
