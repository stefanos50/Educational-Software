using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LearningSoftware
{
    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string path = "AudioSettings.txt";
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
                File.WriteAllText(path, "on");
            }

            path = "UserID.txt";
            if (!File.Exists(path))
            {
                Application.Run(new TheoryForm("-1","11"));
            }
            else
            {
                Application.Run(new MainForm());
            }
        }
    }
}
