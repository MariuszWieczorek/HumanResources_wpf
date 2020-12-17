using HumanResources.Commands;
using HumanResources.Models;
using HumanResources.Models.Domains;
using HumanResources.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HumanResources.ViewModels
{
    class AddEditEmployeeViewModel : ViewModelBase
    {

        private Repository _repository = new Repository();
        public ICommand CloseCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }

        private EmployeeWrapper _employee;
        public EmployeeWrapper Employee
        {
            get { return _employee; }
            set
            {
                _employee = value;
                OnPropertyChanged();
            }
        }

        private bool _isUpdate;
        public bool IsUpdate
        {
            get { return _isUpdate; }
            set
            {
                _isUpdate = value;
                OnPropertyChanged();
            }
        }

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


        private void InitDepartments()
        {

            var departments = _repository.GetDepartments();
            departments.Insert(0, new Department { Id = 0, Name = "--" });

            // tworzymy nowy obiekt ObservableCollection i przekazujemy
            // listę jako parametr
            Departments = new ObservableCollection<Department>(departments);
            if(Employee != null)
                Employee.Department.Id = Employee.Department.Id; 
        }

     

        public AddEditEmployeeViewModel(EmployeeWrapper employee = null)
        {
            CloseCommand = new RelayCommand(Close);
            ConfirmCommand = new RelayCommand(Confirm);

            if (employee == null)
            {
                Employee = new EmployeeWrapper();
            }
            else
            {
                Employee = employee;
                IsUpdate = true;
            }

            InitDepartments();
        }

        private void Close(object obj)
        {
            // Obiekt rzutujemy na Window
            CloseWindow(obj as Window);
        }

        private void Confirm(object obj)
        {
            if (!Employee.IsValid)
                return;

            if (!IsUpdate)
                AddNewEmployee();
            else
                UpdateEmployee();

            CloseWindow(obj as Window);
        }

        private void AddNewEmployee()
        {
            _repository.AddEmployee(Employee);
        }

        private void UpdateEmployee()
        {
            _repository.UpdateEmployee(Employee);
        }

        private void CloseWindow(Window window)
        {
            window.Close();
        }


        /// <summary>
        /// Archiwalna metoda
        /// </summary>
          
    }
}
