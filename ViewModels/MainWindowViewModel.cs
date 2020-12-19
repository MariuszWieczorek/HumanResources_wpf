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
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Windows;

namespace HumanResources.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        
        public ICommand AddEmployeeCommand { get; set; }
        public ICommand EditEmployeeCommand { get; set; }
        public ICommand DeleteEmployeeCommand { get; set; }
        public ICommand RefreshEmployeesCommand { get; set; }
        public ICommand ConectionConfigurationCommand { get; set; }

        public ICommand ComboBoxChanged { get; set; }
        public ICommand CheckBoxChanged { get; set; }

        public ICommand LoadedWindowCommand { get; set; }
        
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


        private bool _showReleasedEmployees;

        public bool ShowReleasedEmployees
        {
            get { return _showReleasedEmployees; }
            set { _showReleasedEmployees = value; }
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

            RefreshEmployeesCommand = new RelayCommand(RefreshEmployees);
            AddEmployeeCommand = new RelayCommand(AddEditEmployee);
            EditEmployeeCommand = new RelayCommand(AddEditEmployee, CanEditDeleteEmployee);
            DeleteEmployeeCommand = new AsyncRelayCommand(DeleteEmployee, CanEditDeleteEmployee);

            ConectionConfigurationCommand = new RelayCommand(ConectionConfiguration);
            LoadedWindowCommand = new RelayCommand(MainWindow_Loaded);
            ComboBoxChanged = new RelayCommand(ComboBox_LostFocus);
            CheckBoxChanged = new RelayCommand(CheckBox_Click);

            // Gdy test połączenie wypadnie negatywnie
            // wywołane zostaje okienko konfiguracyjne połączenia SQL
            // po zapisie ustawień w tym okienku zostaje wymuszony restart aplikacji
            // po anulowaniu nastepuje zamknięcie aplikacji
            if (!DbHelper.ConnectionSettingsTest())
            {
                var connectionConfigurationWindow = new ConnectionConfigurationView();
                connectionConfigurationWindow.ShowDialog();
            }

            var loginView = new LoginWindowView();
            loginView.ShowDialog(); //koniecznie ShowDialog() !!!


            InitDepartments();
            RefreshEmployeesList();
        }

        private void CheckBox_Click(object obj)
        {
            RefreshEmployeesList();
        }

        private void RefreshEmployees(object obj)
        {
            RefreshEmployeesList();
        }

        /// <summary>
        /// Metoda do wywołania po wciśnięciu przycisku Usuń
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private async Task DeleteEmployee(object arg)
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;

            var dialog =
                await metroWindow.ShowMessageAsync("Usuwanie Pracownika",
                $"Czy na pewno chcesz usunąć kartotekę {SelectedEmployee.FirstName} {SelectedEmployee.LastName}",
                MessageDialogStyle.AffirmativeAndNegative);

            if (dialog != MessageDialogResult.Affirmative)
            {
                return;
            }

            _repository.DeleteEmployee(SelectedEmployee.Id);
            RefreshEmployeesList();
        }
        
        // czy aktywny przycisk Usuń i Edycja
        private bool CanEditDeleteEmployee(object obj)
        {
            return SelectedEmployee != null;
        }


        /// <summary>
        /// Metoda do wywołania po wciśnięciu przycisku Edycja lub Dodaj
        /// </summary>
        /// <param name="obj"></param>
        private void AddEditEmployee(object obj)
        {
            var addEditEmployeeWindow = new AddEditEmployeeView(obj as EmployeeWrapper);
            // Subskrybujemy zdarzenie closed okna edycji pracownika
            // Metoda AddEditEmployeeWindow_Closed zostanie wykonana w momencie zamknięcia tego okna
            addEditEmployeeWindow.Closed += AddEditEmployeeWindow_Closed;
            addEditEmployeeWindow.ShowDialog();
            addEditEmployeeWindow.Closed -= AddEditEmployeeWindow_Closed;
        }

        // Metoda do wywołania w momencie zamknięcia okna edycji pracownika
        private void AddEditEmployeeWindow_Closed(object sender, EventArgs e)
        {
            RefreshEmployeesList();
        }

        private void RefreshEmployeesList()
        {
            Employees = new ObservableCollection<EmployeeWrapper>(_repository.GetEmployees(SelectedDepartmentId, ShowReleasedEmployees));
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
            ShowReleasedEmployees = true;
        }

        private void ComboBox_LostFocus(object obj)
        {
            RefreshEmployeesList();
        }

        private void MainWindow_Loaded(object obj)
        {
            return;
        }

        private void ConectionConfiguration(object obj)
        {
            var connectionConfigurationWindow = new ConnectionConfigurationView();
            connectionConfigurationWindow.ShowDialog();
        }
    }
}
