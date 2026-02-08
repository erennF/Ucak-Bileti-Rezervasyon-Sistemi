// uçakSistemi\Program.cs
using System;
using System.Windows.Forms;

namespace uçakSistemi
{
    internal static class Program
    {
        /// <summary>
        /// Uygulamanın ana girdi noktası.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           

                
                VeritabaniHelper.InitializeDatabase();

                Application.Run(new Form1());
            }
        }
    }
