using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Trackrypto.View;
using Trackrypto.ViewModel;

namespace Trackrypto
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            StartupUri = new Uri("View/MainWindow.xaml", UriKind.Relative);
            Run();
        }
    }
}
