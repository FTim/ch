using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChClient.Models
{
    public class Project
    {
        public string Name { get; set; }
        public string Leader { get; set; }
        public string Goal { get; set; }
        public string Description { get; set; }
        public List<string> ProjectPlanImgPaths { get; set; }

        public Project()
        {
            ProjectPlanImgPaths = new List<string>();
        }
    }
}
