using BAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnMehta.WebPortal.EmailScheduler.Comman;
using UnMehta.WebPortal.EmailScheduler.Services;

namespace UnMehta.WebPortal.EmailScheduler
{
    public partial class Service1 : ServiceBase
    {
        private Timer scheduleTrigger = null;
        public Service1()
        {
            InitializeComponent();
        }

        private void SchedulerTrigger()
        {

            string mode = Functions.strMode.ToUpper();

            DateTime scheduledTime = DateTime.MinValue;

            if (mode == "DAILY")
            {
                scheduledTime = DateTime.Parse(Functions.strScheduledTime);
                //Get the Scheduled Time from AppSettings.
                if (DateTime.Now > scheduledTime)
                {
                    //If Scheduled Time is passed set Schedule for the next day.
                    scheduledTime = scheduledTime.AddDays(1);
                }
            }

            if (mode.ToUpper() == "INTERVAL")
            {
                //Get the Interval in Minutes from AppSettings.
                int intervalMinutes = Convert.ToInt32(Functions.strIntervalMinutes);

                //Set the Scheduled Time by adding the Interval to Current Time.
                scheduledTime = DateTime.Now.AddMinutes(intervalMinutes);
                if (DateTime.Now > scheduledTime)
                {
                    //If Scheduled Time is passed set Schedule for the next Interval.
                    scheduledTime = scheduledTime.AddMinutes(intervalMinutes);
                }
            }
            TimeSpan timeSpan = scheduledTime.Subtract(DateTime.Now);
            string schedule = string.Format("{0} day(s) {1} hour(s) {2} minute(s) {3} seconds(s)", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

            ErrorLogger.ERROR("", "Service scheduled to run after: " + schedule + " {0}");


            //Get the difference in Minutes between the Scheduled and Current Time.
            int dueTime = Convert.ToInt32(timeSpan.TotalMilliseconds);

            //Change the Timer's Due Time.
            scheduleTrigger.Change(dueTime, Timeout.Infinite);
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                scheduleTrigger = new Timer(new TimerCallback(SchedularCallback));

                this.SchedulerTrigger();
            }
            catch (Exception ex)
            {
                ErrorLogger.ERROR("Service Error on: ", ex.Message + ex.StackTrace);

                //Stop the Windows Service.
                using (System.ServiceProcess.ServiceController serviceController = new System.ServiceProcess.ServiceController("SimpleService"))
                {
                    serviceController.Stop();
                }
            }

        }


        private void SchedularCallback(object e)
        {
            ErrorLogger.ERROR("Service Start ", "Start");

            EmailSend objStart = new EmailSend();
            objStart.StartEmail();
         
            ErrorLogger.ERROR("Service Start ", "End");
            this.SchedulerTrigger();
        }


        protected override void OnStop()
        {
            ErrorLogger.ERROR("Service Stop Trigger ", "Done");
        }
    }
}
