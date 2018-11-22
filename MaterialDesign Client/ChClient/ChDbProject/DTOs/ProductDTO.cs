using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChDbProject.DTOs
{
    public class ProductDTO
    {
        public string ReactionName { get; set; }
        public double Ratio { get; set; }
        public double MW { get; set; }

        public Product TransformToProduct()
        {
            Product result = new Product();
            result.Ratio = Ratio;
            result.MW = MW;

            return result;

        }
    }
}
