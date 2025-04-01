using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using UnMehta.Admission.PaymentScheduler.Services;

namespace UnMehta.Admission.PaymentScheduler
{
    static class Program
    {
        static void Main(string[] args)
        {

            //StudentRegStatusUpdate objStart = new StudentRegStatusUpdate();
            //objStart.StartEmail();

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service1()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
