﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TenEnv.TestTool
{
    public class Test
    {
        public enum FinishCodes { OK, Fail }

        private const string Title = "Windows 10 Environment: Test";

        public static bool Perform()
        {
            System.Windows.Forms.MessageBox.Show("You are running this program for the first time, so some features needs to be tested on your computer.", Title);

            return true;
        }
    }
}
