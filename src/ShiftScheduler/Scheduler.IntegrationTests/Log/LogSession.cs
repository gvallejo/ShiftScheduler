using Gurock.SmartInspect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.IntegrationTests.Log
{
    public sealed class LogSession
    {
        private const string APPNAME = "Auto";
        private const string CONNECTIONS =
            "pipe(reconnect=true, reconnect.interval=1s)";
        private const string SESSION = "Scheduler.IntegrationTests";

        private static Session fMain;
        private static SmartInspect fSi;

        private LogSession() { }

        static LogSession()
        {
            fSi = new SmartInspect(APPNAME);
            fSi.Connections = CONNECTIONS;
            fMain = fSi.AddSession(SESSION, true);
            if (!fSi.Enabled)
                fSi.Enabled = true;
        }

        /// <summary>
        ///   Automatically created SmartInspect instance.
        /// </summary>
        /// <!--
        /// <remarks>
        ///   The <link SmartInspect.Connections, connections string> is set
        ///   to "pipe(reconnect=true, reconnect.interval=1s)". Please see
        ///   Protocol.IsValidOption for information on the used options. The
        ///   <link SmartInspect.AppName, application name> is set to "Auto".
        ///
        ///   <b>Please note that the default connections string has been
        ///   changed in SmartInspect 3.0</b>. In previous versions, the
        ///   default connections string was set to "tcp()".
        /// </remarks>
        /// -->

        public static SmartInspect Si
        {
            get { return fSi; }
        }

        /// <summary>
        ///   Automatically created Session instance.
        /// </summary>
        /// <!--
        /// <remarks>
        ///   The <link Session.Name, session name> is set to "Main" and
        ///   the <link Session.Parent, parent> to SiAuto.Si.
        /// </remarks>
        /// -->

        public static Session Main
        {
            get { return fMain; }
        }
    }
}
