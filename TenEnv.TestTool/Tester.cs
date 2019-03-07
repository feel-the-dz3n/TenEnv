using System;
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

            try
            {
                var result = new WpfTest().ShowDialog();
                if (!(bool)result)
                {
                    throw new Exception("User can't read text from the 1st step of the test");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), Title, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
    }
}
