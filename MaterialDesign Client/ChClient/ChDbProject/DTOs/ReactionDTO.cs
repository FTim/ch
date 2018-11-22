using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChDbProject.DTOs
{
    public class ReactionDTO
    {
        public string Code { get; set; }
        public string Chemist { get; set; }
        public string Chiefchemist { get; set; }
        public string Project { get; set; }
        public string Laboratory { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ClosureDate { get; set; }
        public string PreviousStep { get; set; }
        public string Literature { get; set; }
        public byte[] ReactionImg { get; set; }
        //public string ReactionImgPath { get; set; }

        public StartingMaterialDTO StartingMaterial { get; set; }
        public List<ReagentDTO> Reagents { get; set; }
        public List<SolventDTO> Solvents { get; set; }
        public List<ProductDTO> Products { get; set; }

        public string Procedure { get; set; }
        public string Yield { get; set; }
        public string Observation { get; set; }
        public List<byte[]> ObservationImgs { get; set; }

        public bool IsSketch { get; set; }

        public ReactionDTO()
        {
            Reagents = new List<ReagentDTO>();
            Solvents = new List<SolventDTO>();
            Products = new List<ProductDTO>();
            ObservationImgs = new List<byte[]>();
        }

        public Reaction TransformToReaction()
        {
            Reaction result = new Reaction();

            result.ReactionCode = Code;
            result.Laboratory = Laboratory;
            result.StartDate = StartDate;
            result.ReactionImg = ReactionImg;

            if (string.IsNullOrEmpty(Literature))
            {
                result.Literature = "-";
            }
            else result.Literature = Literature;

            if (PreviousStep == "-") PreviousStep = null;
            
            
            result.Sketch = IsSketch;
            if (!IsSketch)
            {
                result.ProcedureText = Procedure;
                result.Observation = Observation;
                result.Yield = Yield;
                result.ClosureDate = ClosureDate;
            }

            return result;
        }
    }
}
