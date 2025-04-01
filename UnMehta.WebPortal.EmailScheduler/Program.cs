using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using UnMehta.WebPortal.EmailScheduler.Services;

namespace UnMehta.WebPortal.EmailScheduler
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            //Manually Debug Service
            //EmailSend objStart = new EmailSend();
            //objStart.StartEmail();


            //Live Service
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service1()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
