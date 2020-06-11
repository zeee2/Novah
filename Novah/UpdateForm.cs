using MetroFramework.Forms;
using Novah.core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinHttp;

namespace Novah
{
    public partial class UpdateForm : MetroForm
    {
        public UpdateForm()
        {
            InitializeComponent();
        }

        private void UpdateForm_Load(object sender, EventArgs e)
        {
            updateStart();
        }
        string ver = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        private string winhttp(string http, string referer)
        {
            WinHttpRequest win = new WinHttpRequest();
            try
            {
                win.Open("GET", http, true);
                win.SetRequestHeader("Referer", referer);
                win.Send();
                win.WaitForResponse();
            }
            catch { MessageBox.Show(this, "\rServer Connection Error", "Novah"); Environment.Exit(0); }
            string result = Encoding.UTF8.GetString(win.ResponseBody);
            return result;
        }

        private void updateStart()
        {
            try
            {
                string http = "https://debian.moe/static/switcher/new/dllink.txt";
                string referer = "https://debian.moe/";
                string result = winhttp(http, referer);
                WebClient webClient = new WebClient();
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                string path = Environment.CurrentDirectory;
                string path2 = path + @"\" + ver + ".exe";
                webClient.DownloadFileAsync(new Uri(result), path2);
            }
            catch (Exception ex)
            {
                LogCore.Log(ex);

                MessageBox.Show("Update Failled, \r\rPlease Send Discrod Nerina#4444 the Switcher Logs", "Novah", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string filepath = Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\novahlog.txt";
                Process.Start(filepath);
                Environment.Exit(0);
            }

        }

        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            metroProgressSpinner1.Value = e.ProgressPercentage;
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            MessageBox.Show(this, "\rUpdate Successful", "Novah");
            try
            {
                string path = Environment.CurrentDirectory;
                string path2 = path + @"\" + ver + ".exe";
                Process ps = new Process();
                ps.StartInfo.FileName = ver + ".exe";
                ps.StartInfo.WorkingDirectory = path;
                ps.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                ps.Start();
            }
            catch { }
            Environment.Exit(0);
        }
    }
}
