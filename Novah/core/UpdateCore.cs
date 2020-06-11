using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinHttp;

namespace Novah.core
{
    class UpdateCore
    {
        public static string verchk = "0";

        public static void version(string version)
        {
            string result = PhpCore.php("https://debian.moe/static/switcher/new/ver.txt");
            if (result != version)
            {
                verchk = "0";
            }
            if (result == version)
            {
                verchk = "1";
            }
        }

    }
}
