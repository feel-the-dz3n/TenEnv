using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace TenEnv.Core
{
    public class ManAttach
    {
        private const int Timeout = 2000;

        public Process ManProcess { get; private set; }
        public Process AppProcess { get; private set; }

        /// <summary>
        /// Attach TenApp process to TenEnv.Manager process
        /// </summary>
        /// <param name="me">TenApp Process</param>
        /// <param name="man">TenEnv.Manager Process</param>
        public ManAttach(Process me, Process man)
        {
            ManProcess = man;
            AppProcess = me;

            // If Manager not found
            if (ManProcess == null && !Debugger.IsAttached)
                AppProcess.Kill();

            new Thread(CheckThread).Start();
        }

        private void CheckThread()
        {
            while (true)
            {
                Thread.Sleep(Timeout);

                if (ManProcess.HasExited && !Debugger.IsAttached)
                    AppProcess.Kill();
            }
        }
    }
}
