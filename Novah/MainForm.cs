using MetroFramework.Forms;
using Novah.core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Novah
{
    public partial class MainForm : MetroForm
    {
        #region variables
        string ver = "1.0.1";
        string server_url = "https://debian.moe";
        public static string certthumbprint = "5dbe360599200f97954fd3d0f4d183dcce20f5b1";
        string GetServerIP = PhpCore.php("https://debian.moe/static/switcher/new/ip.txt");
        string ServerIP = "null";
        #endregion

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                UpdateCore.version(ver);
                if (UpdateCore.verchk == "1")
                {
                    if (GetServerIP == "404 page not found")
                    {
                        ServerIP = GetServerIP;
                        GetServer();
                    }
                    if (GetServerIP != "404 page not found")
                    {
                        ServerIP = "34.85.96.178";
                        GetServer();
                    }
                }
                if (UpdateCore.verchk == "0")
                {
                    UpdateForm frm = new UpdateForm();
                    Hide();
                    ShowInTaskbar = false;
                    frm.Show();
                }
            }
            catch (Exception ex)
            {
                LogCore.Log(ex);

                MessageBox.Show("Error! \r\rPlease Send Discrod Nerina#4444 the Switcher Logs", "Novah", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string filepath = Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\novahlog.txt";
                Process.Start(filepath);
                Environment.Exit(0);
            }
        }

        #region Link
        private void materialLabel3_Click(object sender, EventArgs e)
        {
            Process.Start(server_url);
        }

        private void materialLabel2_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/Nerina1241/NOVAH-DebianSwitcher-");
        }
        #endregion

        public void DisableButtons()
        {
            flatButton1.Enabled = false;
            flatButton2.Enabled = false;
        }

        public void EnableButtons()
        {
            flatButton1.Enabled = true;
            flatButton2.Enabled = true;
        }

        public void GetServer()
        {
            try
            {
                DisableButtons();
                bool ServerChk = HostsCore.gethosts().Any(x => x.Contains("osu.ppy.sh") && !x.Contains("#"));
                string serverChk = Convert.ToString(ServerChk);
                if (serverChk == "True")
                {
                    materialLabel1.Text = "Welcome To Debian!";
                }
                if (serverChk == "False")
                {
                    materialLabel1.Text = "Connecting Debian?";
                }
            }
            catch (Exception ex)
            {
                LogCore.Log(ex);

                MessageBox.Show("Error! \r\rPlease Send Discrod Nerina#4444 the Switcher Logs", "Novah", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string filepath = Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\novahlog.txt";
                Process.Start(filepath);
                Environment.Exit(0);
            }
            finally { EnableButtons(); }
        }

        #region Click Events
        private void flatButton1_Click(object sender, EventArgs e)
        {
            Application.DoEvents();

            CertificateCore.UninstallCertificates();

            List<string> hosts = HostsCore.gethosts().ToList();
            hosts.RemoveAll(x => x.Contains(".ppy.sh"));
            HostsCore.changehosts(hosts.ToArray());
            string[] osu_domains = new string[]
            {
                "osu.ppy.sh",
                "c.ppy.sh",
                "c1.ppy.sh",
                "c2.ppy.sh",
                "c3.ppy.sh",
                "c4.ppy.sh",
                "c5.ppy.sh",
                "c6.ppy.sh",
                "ce.ppy.sh",
                "a.ppy.sh",
                "i.ppy.sh",
            };
            hosts = HostsCore.gethosts().ToList();
            foreach (string domain in osu_domains)
            {
                hosts.Add(ServerIP + " " + domain);
            }
            HostsCore.changehosts(hosts.ToArray());

            CertificateCore.InstallCertificate();

            materialLabel1.Text = "Welcome To Debian!";
        }

        private void flatButton2_Click(object sender, EventArgs e)
        {
            try
            {
                string[] _hosts = HostsCore.gethosts();
                _hosts = _hosts.Where(x => !x.Contains(".ppy.sh")).ToArray();
                HostsCore.changehosts(_hosts);
                materialLabel1.Text = "Connecting Debian?";
                CertificateCore.UninstallCertificates();
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
        #endregion
    }
}
