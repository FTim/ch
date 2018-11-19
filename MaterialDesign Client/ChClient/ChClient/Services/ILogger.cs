using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChClient.Services
{
    public interface ILogger
    {
        void Write(object sender, string message, string level);
        void Write(object sender, string message);
    }
        
}
