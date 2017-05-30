using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CameraController
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

            string settingsFilename = Path.Combine(Application.UserAppDataPath, "settings.json");
            Settings settings = LoadSettings(settingsFilename);

            Application.Run(new MainWindow(settings));
        }

        private static Settings LoadSettings(string settingsFilename)
        {
            Settings settings = null;
            if (File.Exists(settingsFilename))
            {
                using (var reader = new StreamReader(settingsFilename, Encoding.UTF8))
                {
                    settings = Settings.Load(reader);
                }
            }

            if (settings == null)
                settings = new Settings();

            return settings;
        }
    }
}
