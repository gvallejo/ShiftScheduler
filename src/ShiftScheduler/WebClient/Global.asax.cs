using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Security;
    using System.Web.SessionState;
    using DevExpress.Web.ASPxClasses;
using Scheduler.Core.Scheduling;
using WebClient.DependencyInjection;
using WebClient.Log;


    namespace WebClient 
    {
        public class Global_asax : System.Web.HttpApplication 
        {
            void Application_Start(object sender, EventArgs e) 
            {
                IoC.IoC.Initialize(new DependencyResolver());

                IScheduler _scheduler = IoC.IoC.Resolve<IScheduler>();
                DevExpress.Web.ASPxClasses.ASPxWebControl.CallbackError += new EventHandler(Application_Error);
            }

            void Application_End(object sender, EventArgs e) {
                // Code that runs on application shutdown
            }

            void Application_Error(object sender, EventArgs e) {
                // Code that runs when an unhandled error occurs
                LogSession.Main.LogError(e.ToString());
            }

            void Session_Start(object sender, EventArgs e) {
                // Code that runs when a new session is started
            }

            void Session_End(object sender, EventArgs e) {
                // Code that runs when a session ends. 
                // Note: The Session_End event is raised only when the sessionstate mode
                // is set to InProc in the Web.config file. If session mode is set to StateServer 
                // or SQLServer, the event is not raised.
            }
        }
    }