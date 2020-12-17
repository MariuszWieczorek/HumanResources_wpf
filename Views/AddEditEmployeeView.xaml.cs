using HumanResources.Models;
using HumanResources.Models.Wrappers;
using HumanResources.ViewModels;
using HumanResources;
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
    /// Interaction logic for AddEditStudentView.xaml
    /// </summary>
    public partial class AddEditEmployeeView : MetroWindow
    {
        public AddEditEmployeeView(EmployeeWrapper employee = null )
        {
            InitializeComponent();
            DataContext = new AddEditEmployeeViewModel(employee);
        }
    }
}
