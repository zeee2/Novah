using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Novah.core
{
    class CertificateCore
    {
        static string ctp = MainForm.certthumbprint;

        public static Task<bool> GetStatusAsync()
        {
            return Task.Run(() =>
            {
                X509Store x509Store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
                x509Store.Open(OpenFlags.ReadOnly);
                bool found = x509Store.Certificates.Find(X509FindType.FindByThumbprint, ctp, true).Count >= 1;
                x509Store.Close();
                return found;
            });
        }

        public static void InstallCertificate()
        {
            try
            {
                X509Store x509Store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
                x509Store.Open(OpenFlags.ReadWrite);
                var certificate = new X509Certificate2(Properties.Resources.cert1);
                x509Store.Add(certificate);
                x509Store.Close();
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

        public static void UninstallCertificates()
        {
            try
            {
                X509Store x509Store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
                x509Store.Open(OpenFlags.ReadWrite);
                foreach (X509Certificate2 certificate in x509Store.Certificates.Find(X509FindType.FindByThumbprint, ctp, true))
                {
                    try
                    {
                        x509Store.Remove(certificate);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                x509Store?.Close();
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
