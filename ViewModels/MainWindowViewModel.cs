using HumanResources.Commands;
using HumanResources.Views;
using HumanResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HumanResources.Models.Wrappers;
using System.Collections.ObjectModel;
using HumanResources.Models.Domains;

namespace HumanResources.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        public ICommand ConectionConfigurationCommand { get; set; }
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand ComboBoxChanged { get; set; }
        private Repository _repository = new Repository();

        
        // właściwość przechowująca wybranego pracownika
        private EmployeeWrapper _selectedEmployee;
        public EmployeeWrapper SelectedEmployee
        {
            get
            {

                return _selectedEmployee;
            }
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged();
            }
        }

        // używamy zamiast List<> zachowuje się jak zwykła lista
        // implementuje dodatkowo interfejsy INotifyCollectionChanged, INotifyPropertyChanged
        // dzięki temu datagrid będzie informowany o tym
        // czy jekiś element został dodany lub zmieniony
        private ObservableCollection<EmployeeWrapper> _employees;
        public ObservableCollection<EmployeeWrapper> Employees
        {
            get { return _employees; }
            set
            {
                _employees = value;
                OnPropertyChanged();
            }
        }

        // właściwość przechowująca wybrany dział
        // po stronie widoku zbindowana z SelectedValue ComboBoxa
        private int _selectedDepartmentId;
        public int SelectedDepartmentId
        {
            get { return _selectedDepartmentId; }
            set
            {
                _selectedDepartmentId = value;
                OnPropertyChanged();
            }
        }

        // właściwość przechowująca listę działów
        // po stronie widoku zbindowana z ItemsSource ComboBoxa
        private ObservableCollection<Department> _departments;
        public ObservableCollection<Department> Departments
        {
            get { return _departments; }
            set
            {
                _departments = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            ConectionConfigurationCommand = new RelayCommand(ConectionConfiguration);
            LoadedWindowCommand = new RelayCommand(MainWindow_Loaded);
            ComboBoxChanged = new RelayCommand(ComboBox_LostFocus);

            // Gdy test połączenie wypadnie negatywnie
            // wywołane zostaje okienko konfiguracyjne połączenia SQL
            // po zapisie ustawień w tym okienku zostaje wymuszony restart aplikacji
            // po anulowaniu nastepuje zamknięcie aplikacji
            if (!DbHelper.ConnectionSettingsTest())
            {
                var connectionConfigurationWindow = new ConnectionConfigurationView();
                connectionConfigurationWindow.ShowDialog();
            }

            InitDepartments();

        }

        /// <summary>
        /// Pobiera słownik grup z bazy danych służący do filtrowania danych
        /// Dodaje rekord o Id == 0, któy oznacza, że nie filtrujemy po grupie
        /// </summary>
        private void InitDepartments()
        {
            var departments = _repository.GetDepartments();
            // wstawiamy grupę domyślną
            departments.Insert(0, new Department { Id = 0, Name = "Wszystkie" });

            // tworzymy nowy obiekt ObservableCollection i przekazujemy
            // listę jako parametr do konstruktora
            Departments = new ObservableCollection<Department>(departments);
            SelectedDepartmentId = 0;
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
