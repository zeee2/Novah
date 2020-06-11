using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinHttp;

namespace Novah.core
{
    class PhpCore
    {
        public static string php(string url)
        {
            WinHttpRequest win = new WinHttpRequest();
            try
            {
                win.Open("GET", url);
                win.Send();
                win.WaitForResponse();
            }
            catch
            {
                MessageBox.Show("Connection Error", "NOVAH", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
            return win.ResponseText;
        }
    }
}
