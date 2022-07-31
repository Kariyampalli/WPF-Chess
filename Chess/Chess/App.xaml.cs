//// <copyright file="App.xaml.cs" company ="FH Wr.Neustadt">
//// Copyright by Christy Kariyampalli. All rights reserved
//// </copyright>
//// <summary>
//// App class of the program.
//// </summary>
namespace Chess
{ 
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using Chess.View;

    /// <summary>
    /// App class for starting the program.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Starts the main window/ the app.
        /// </summary>
        /// <param name="sender">
        /// Object sender.
        /// </param>
        /// <param name="e">
        /// Arguments on start up.
        /// </param>
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
