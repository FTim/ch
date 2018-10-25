using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChClient.Models
{
    public class StartingMaterialErrorInfo
    {
        public string NoStartingMaterialSelectedError { get; set; }
        public string HasBothValuesError { get; set; }
        public string CannotConvertNumberError { get; set; }
    }
}
