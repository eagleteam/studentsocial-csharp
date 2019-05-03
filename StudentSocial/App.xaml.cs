using StudentSocial.Common;
using StudentSocial.GUI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace StudentSocial
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (!Paths.checkToken())
            {
                Paths.createdPaths();
                //Process.Start(Application.ResourceAssembly.Location);
                //Application.Current.Shutdown();
            }
            WMain main = new WMain();
            main.Show();
        }
    }
}
