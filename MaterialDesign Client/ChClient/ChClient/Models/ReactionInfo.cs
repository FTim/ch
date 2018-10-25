using System;
using System.Collections.Generic;

namespace ChClient.Models
{
    public class ReactionInfo
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
        public string ReactionImgPath { get; set; }

        public StartingMaterial StartingMaterial { get; set; }
        public List<Reagent> Reagents { get; set; }
        public List<Solvent> Solvents { get; set; }
        public List<Product> Products { get; set; }

        public string Procedure { get; set; }
        public string Yield { get; set; }
        public string Observation { get; set; }
        public List<string> ObservationImgPaths { get; set; }

        public bool IsSketch { get; set; }

        public string SaveLocation { get; set; }

        
        public ReactionInfo()
        {
            ObservationImgPaths = new List<string>();
        }

        public ReactionErrorInfo ValidateHeader()
        {
            ReactionErrorInfo result = new ReactionErrorInfo();

            if (String.IsNullOrEmpty(Code))
            {
                result.ReactionCodeError = "Reaction code is required!";
            }
            if (IsSketch)
            {
                result.ClosureDateIgnoreNote = "Sketch option checked, closure date wil not be in report!";
            }
            else
            {
                //start<closure -> res<0
                //start=closure -> res=0
                //start>closure -> res>0
                if (DateTime.Compare(StartDate, ClosureDate) > 0)
                {
                    result.ClosureDateError = "Closure date is earlier than start date!";
                }
            }
            if (String.IsNullOrEmpty(ReactionImgPath))
            {
                result.ReactionImageError = "Reaction image is required!";
            }

            return result;
        }
    }
}
