using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using MetroFramework.Forms;
using System.Diagnostics;

namespace Novah.core
{
    class HostsCore
    {
        public static string[] gethosts()
        {
            return File.ReadAllLines(Environment.SystemDirectory + @"\drivers\etc\hosts");
        }

 

        public static void changehosts(string[] hosts)
        {
            try
            {
                try
                {
                    File.WriteAllLines(Environment.SystemDirectory + @"\drivers\etc\hosts", hosts);
                }
                catch (Exception ex)
                {
                    DialogResult dr = MessageBox.Show(ex.Message, "NOVAH", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    if (dr == DialogResult.Retry)
                    {
                        try
                        {
                            File.WriteAllLines(Environment.SystemDirectory + @"\drivers\etc\hosts", hosts);
                        }
                        catch
                        {
                            LogCore.Log(ex);
                            MessageBox.Show("Error! \r\rPlease Send Discrod Nerina#4444 the Switcher Logs", "NOVAH", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            string filepath = Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\novahlog.txt";
                            Process.Start(filepath);
                            Environment.Exit(0);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogCore.Log(ex);
                MessageBox.Show("Error! \r\rPlease Send Discrod Nerina#4444 the Switcher Logs", "NOVAH", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string filepath = Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\novahlog.txt";
                Process.Start(filepath);
                Environment.Exit(0);
            }
        }
    }
}
