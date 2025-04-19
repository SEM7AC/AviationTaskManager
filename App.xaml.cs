using AviationTaskManager.Models;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Windows;

namespace AviationTaskManager
    {
    /// <summary>
    /// Interaction logic for App.xaml
    /// 
    /// Initialize the database before launching the UI
    /// 
    /// </summary>
    public partial class App : Application
        {
        protected override void OnStartup(StartupEventArgs e)
            {
            base.OnStartup(e);

            var splashScreen = new Views.SplashScreen();
            splashScreen.Show();

            // Log the application startup process for debugging purposes
            System.Diagnostics.Debug.WriteLine("SplashScreen launched. Application starting... (App.xaml.cs)");
            }
        }
    }
