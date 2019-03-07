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
            {
                var q = System.Windows.Forms.MessageBox.Show(
                    "You can't start Windows 10 Environment applications without manager. Please, use the program called 'TenEnv.Manager.exe'. If you don't want to start manager, you can attach debugger instead. Do it?",
                    "Windows 10 Environment Core", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Exclamation);

                if(q == System.Windows.Forms.DialogResult.No)
                    AppProcess.Kill();
                else
                    Debugger.Launch();
            }

            new Thread(CheckThread).Start();
        }

        private void CheckThread()
        {
            while (true)
            {
                if (ManProcess.HasExited && !Debugger.IsAttached)
                    AppProcess.Kill();

                Thread.Sleep(Timeout);

            }
        }
    }
}
