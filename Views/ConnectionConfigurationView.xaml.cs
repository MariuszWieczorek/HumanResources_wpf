using HumanResources.ViewModels;
using MahApps.Metro.Controls;
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
using System.Windows.Shapes;

namespace HumanResources.Views
{
    /// <summary>
    /// Interaction logic for ConnectionConfigurationView.xaml
    /// </summary>
    public partial class ConnectionConfigurationView : MetroWindow
    {
        public ConnectionConfigurationView()
        {
            InitializeComponent();
            DataContext = new ConnectionConfigurationViewModel();
        }
    }
}
