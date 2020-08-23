using Dapper.Contrib.Extensions;
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

            ConfigureDapper();

            StartupUri = new Uri("View/MainWindow.xaml", UriKind.Relative);
            Run();
        }

        private void ConfigureDapper()
        {
            DapperExtensions.DapperExtensions.SqlDialect = new DapperExtensions.Sql.MySqlDialect();
            SqlMapperExtensions.TableNameMapper = (type) => {
                //use exact name
                return type.Name;
            };
        }
    }
}
