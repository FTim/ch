using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChDbProject.DTOs
{
    public class ProjectDTO
    {
        
        public string Name { get; set; }
        public string Leader { get; set; }
        public string Goal { get; set; }
        public string Description { get; set; }
        public byte[] PlanImg { get; set; }

        public Project TransformToProject()
        {
            Project result = new Project();
            result.Name = Name;
            
            result.Goal = Goal;
            result.Description = Description;
            return result;
        }
    }
    
}
