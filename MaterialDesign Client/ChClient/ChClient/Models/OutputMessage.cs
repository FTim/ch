using ChClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChClient.Models
{
    public class OutputMessage
    {
        private string _message;
        public string Message
            {
            get {
                return _message;
            }
            set {
                _message = value;
                FullMessage = DateTime.Now.ToString() + " - " + Message;
            }
        }
        public string Level { get; set; }

        public string FullMessage { get; set; }

    }
}
