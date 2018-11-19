using ChClient.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ChClient.Models
{
    public class Project
    {
        public string Name { get; set; }
        public string Leader { get; set; }
        public string Goal { get; set; }
        public string Description { get; set; }
        public string CurrentPlan { get; set; }
        public List<string> ProjectPlanImgPaths { get; set; }
        public List<byte[]> ProjectPlanByreArrays { get; set; }
        public byte[] LastPlan { get; set; }

        public Project()
        {
            ProjectPlanImgPaths = new List<string>();
            ProjectPlanByreArrays = new List<byte[]>();
        }

        public List<OutputMessage> Validate(List<string> userlist)
        {
            List<OutputMessage> result = new List<OutputMessage>();
            
            if (string.IsNullOrEmpty(Name))
            {
                result.Add(new OutputMessage { Message = "Project name is required!", Level = "error" });
                //result.NameError = "Project name is required!";
            }
            
            if (string.IsNullOrEmpty(Leader))
            {
                result.Add(new OutputMessage { Message = "Leader name is required!", Level = "error" });
                
            }
            else
            {
                if (!userlist.Contains(Leader))
                {
                    result.Add(new OutputMessage { Message = "Leader is not in database!", Level = "error" });
                    
                }
            }


            if (string.IsNullOrEmpty(Goal))
            {
                result.Add(new OutputMessage { Message = "Goal is required!", Level = "error" });
                
            }
            else
            {
                if (Goal.Length > 50)
                {
                    result.Add(new OutputMessage { Message = "Goal text is too long!", Level = "error" });
                    
                }
            }

            if (string.IsNullOrEmpty(Description))
            {
                result.Add(new OutputMessage { Message = "Description is required!", Level = "error" });
                
            }
            if (string.IsNullOrEmpty(CurrentPlan))
            {
                result.Add(new OutputMessage { Message = "Plan image is required!", Level = "error" });
               
            }

            return result;
        }
        
    }
}
