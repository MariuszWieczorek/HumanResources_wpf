using HumanResources.Commands;
using HumanResources.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HumanResources.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        public ICommand ConectionConfigurationCommand { get; set; }
        public ICommand LoadedWindowCommand { get; set; }

        public ICommand ComboBoxChanged { get; set; }


        public MainWindowViewModel()
        {
            ConectionConfigurationCommand = new RelayCommand(ConectionConfiguration);
            LoadedWindowCommand = new RelayCommand(MainWindow_Loaded);
            ComboBoxChanged = new RelayCommand(ComboBox_LostFocus);

            // zostanie utworzone pierwsze zapytanie i utworzone bazy
            using (var context = new ApplicationDbContext())
            {
                var employees = context.Employees.ToList();
            }

        }

        private void ComboBox_LostFocus(object obj)
        {
            // TODO: Oprogramowanie Lost Focus'a Combo Boxa z grupami
            return;
        }

        private void MainWindow_Loaded(object obj)
        {
            // TODO: MainWindow Loaded
            return;
        }

        private void ConectionConfiguration(object obj)
        {
            var connectionConfigurationWindow = new ConnectionConfigurationView();
            connectionConfigurationWindow.ShowDialog();
        }
    }
}
