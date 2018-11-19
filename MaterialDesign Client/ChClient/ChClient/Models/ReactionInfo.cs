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
            Reagents = new List<Reagent>();
            Solvents = new List<Solvent>();
            Products = new List<Product>();
    }

        public List<OutputMessage> Validate(List<string> users, List<string> projectnames, List<string> reactioncodes)
        {
            List<OutputMessage> result = new List<OutputMessage>();
           
            if (string.IsNullOrEmpty(Code))
            {
                result.Add(new OutputMessage { Message = "Reaction code is required!", Level = "error" });

            }
            if (string.IsNullOrEmpty(ReactionImgPath))
            {
                result.Add(new OutputMessage { Message = "Reaction image is required!", Level = "error" });
            }
            if (string.IsNullOrEmpty(Chemist))
            {
                result.Add(new OutputMessage { Message = "Chemist is required!", Level = "error" });
            }
            else
            {
                
                if (!users.Contains(Chemist))
                {
                    result.Add(new OutputMessage { Message = "Chemist is not in the database!", Level = "error" });
                }
            }
            if (string.IsNullOrEmpty(Chiefchemist))
            {
                result.Add(new OutputMessage { Message = "Chiefchemist image is required!", Level = "error" });
            }
            else
            {
                if (!users.Contains(Chiefchemist))
                {
                    result.Add(new OutputMessage { Message = "Chiefchemist is not in the database!", Level = "error" });
                }
            }
            if (string.IsNullOrEmpty(Project))
            {
                result.Add(new OutputMessage { Message = "Project is required!", Level = "error" });
            }
            else
            {
                if (!projectnames.Contains(Project))
                {
                    result.Add(new OutputMessage { Message = "Project is not in the database!", Level = "error" });
                }
            }

            if (IsSketch)
            {
                result.Add(new OutputMessage { Message = "Sketch option checked, closure date, procedure, yield, observation will not be in report.", Level = "info" });
            }
            else
            {
                result.Add(new OutputMessage { Message = "Sketch option unchecked, closure date, procedure, yield, observation are required.", Level = "info" });
                //start<closure -> res<0
                //start=closure -> res=0
                //start>closure -> res>0
                if (DateTime.Compare(StartDate, ClosureDate) > 0)
                {
                    result.Add(new OutputMessage { Message = "Closure date is earlier than start date!", Level = "error" });
                }

                if (string.IsNullOrEmpty(Procedure))
                {
                    result.Add(new OutputMessage { Message = "Procedure is required!", Level = "error" });
                }
                if (string.IsNullOrEmpty(Yield))
                {
                    result.Add(new OutputMessage { Message = "Yield is required!", Level = "error" });

                }
                if (string.IsNullOrEmpty(Observation))
                {
                    result.Add(new OutputMessage { Message = "Observation is required!", Level = "error" });
                }
            }

            if (string.IsNullOrEmpty(PreviousStep) || PreviousStep == "-")
            {
                result.Add(new OutputMessage { Message = "Previous step will be empty in report", Level = "info" });
            }
            else
            {
                if (!reactioncodes.Contains(PreviousStep))
                {
                    result.Add(new OutputMessage { Message = "Previous step is not in the database!", Level = "error" });
                }
            }
            if (string.IsNullOrEmpty(Literature) || Literature == "-")
            {
                result.Add(new OutputMessage { Message = "Literature will be empty in report", Level = "info" });
            }
            
            if (StartingMaterial == null)
            {
                result.Add(new OutputMessage { Message = "No starting material added!", Level = "error" });
            }
            if (Products.Count < 1)
            {
                result.Add(new OutputMessage { Message = "No product added!", Level = "error" });
            }

            if (string.IsNullOrEmpty(SaveLocation))
            {
                result.Add(new OutputMessage { Message = "Using default location for save", Level = "info" });
            }
           

            return result;
        }
    }
}
