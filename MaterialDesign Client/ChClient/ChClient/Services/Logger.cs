using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChClient.Services
{
    public class Logger : ILogger
    {
        private ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        public void Write(object sender, string message, string level)
        {
           
            switch(level)
            {
                case "info":

                    log.Info(sender.ToString()+" - "+message);
                    break;

                case "debug":
                    log.Debug(sender.ToString() + " - " + message);
                    break;

                case "error":
                    log.Error(sender.ToString() + " - " + message);
                    break;

                case "fatal":
                    log.Fatal(sender.ToString() + " - " + message);
                    break;
                default:
                    log.Debug(sender.ToString() + " - " + message);
                    break;
            }            
        }
        public void Write(object sender, string message)
        {
            Write(sender,message, "");
        }
    }
}
