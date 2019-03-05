using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TenEnv.TestTool
{
    class Program
    {

        static int Main()
        {
            if (Test.Perform())
                return (int)Test.FinishCodes.OK;
            else
                return (int)Test.FinishCodes.Fail;
        }
    }
}
