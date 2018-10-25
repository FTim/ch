using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChClient.Models
{
    public class ReactionErrorInfo
    {
        public string ReactionCodeError { get; set; }
        public string ClosureDateError { get; set; }
        public string ReactionImageError { get; set; }
        public string ClosureDateIgnoreNote { get; set; }

        public string NoStartingMaterialError { get; set; }
        public string StartingMaterialValueError { get; set; }

        public string NoReagentError { get; set; }
        public string ReagentValueError { get; set; }

        public string SolventError { get; set; }
        public string SolventValueError { get; set; }

        public string ProductValueError { get; set; }

        public string SketchIgnoreNote { get; set; }

        public string SaveLocationError { get; set; }

        public string SaveReactionError { get; set; }
    }
}
