using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace TenEnv.Core
{
    public class TenAppHelper
    {
        public static Process GetManagerProcessFromArguments(string[] args)
        {
            foreach (var arg in args)
            {
                if (arg.StartsWith("-man:"))
                    return Process.GetProcessById(
                        int.Parse( // convert string to int
                            arg.Split(':')[1] // get integer after : as string
                            )
                            );
            }

            return null; // null if not found
        }
    }
}
