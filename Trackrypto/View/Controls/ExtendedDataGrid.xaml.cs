using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Trackrypto.View.Controls
{
    /// <summary>
    /// Interaction logic for ExtendedDataGrid.xaml
    /// </summary>
    public partial class ExtendedDataGrid : UserControl
    {
        public ExtendedDataGrid()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty DataSourceProperty =
            DependencyProperty.Register("DataSource", typeof(IList<object>), typeof(ExtendedDataGrid), 
                new PropertyMetadata(null, new PropertyChangedCallback(OnSourceChanged)));

        public IList<object> DataSource
        {
            get => (IList<object>)GetValue(DataSourceProperty);
            set => SetValue(DataSourceProperty, value);
        }

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ExtendedDataGrid extendedDataGrid = d as ExtendedDataGrid;
            extendedDataGrid.OnSourceChanged(e);
        }

        private void OnSourceChanged(DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
