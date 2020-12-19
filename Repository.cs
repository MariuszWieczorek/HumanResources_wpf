using HumanResources.Models.Domains;
using HumanResources.Models.Wrappers;
using HumanResources.Models.Converters;
using HumanResources.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Windows;

namespace HumanResources
{
    class Repository
    {
        // Właściwie powinien być obiekt typu Wrapper
        // ale aby uniknąć pisania konwerterów
        public List<Department> GetDepartments()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Departments.ToList();
            }
        }

        // Dane o użytkownikach
        public List<UserWrapper> GetUsers()
        {
            var Users = new List<UserWrapper>
            {
                new UserWrapper {Id = 1, UserName = "Ala", Password = "alamakota"},
                new UserWrapper {Id = 1, UserName = "Mariusz", Password = "a"},
                new UserWrapper {Id = 1, UserName = "a", Password = "a"}
            };

            return Users;
        }

    //
    public List<EmployeeWrapper> GetEmployees(int departmentId = 0, bool showReleasedEmployees = true)
        {
            using (var context = new ApplicationDbContext())
            {



                // dzięki typowi Querable zapytanie nie zostanie od razu wykonane
                var employees = context
                    .Employees
                    .Include(x => x.Department)
                    .AsQueryable();

                if (departmentId != 0)
                    employees = employees.Where(x => x.DepartmentId == departmentId).AsQueryable();

                if (showReleasedEmployees == false)
                    employees = employees.Where(x => x.Released == false).AsQueryable();

                // w tym miejscu wykonamy kwerendę
                // gdyby powyżej było ToList() zapytanie wywołało by się 2 razy

                // musimy przekonwertować na StudentWrapper
                // poniżej przykładowo przekonwertowany pojedynczy student
                // var student = students.First().ToWrapper();

                // musimy przekonwertować na StudentWrapper całą listę
                // czyli chcemy wywołać tę metodę dla każdego studenta z listy
                return employees
                    .ToList()
                    .Select(x => x.ToWrapper())
                    .ToList();


            }
        }

        /// <summary>
        /// Dodawanie Nowego Pracownika
        /// </summary>
        /// <param name="employeeWrapper"></param>
        internal void AddEmployee(EmployeeWrapper employeeWrapper)
        {
            // konwersja z Wrapperów na obiekty domenowe DAO
            var employee = employeeWrapper.toDao();

            // teraz mamy już obiekty domenowe
            using (var context = new ApplicationDbContext())
            {
                // najpierw zapisujemy do bazy aby później odczytać jakie Id nadała baza
                // jeżeli będziemy je dalej potrzebować
                var dbStudent = context.Employees.Add(employee);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Aktualizacja Danych Pracownika
        /// </summary>
        /// <param name="employeeWrapper"></param>
        internal void UpdateEmployee(EmployeeWrapper employeeWrapper)
        {
            // konwersja z Wrapperów na obiekty domenowe DAO
            var employee = employeeWrapper.toDao();
            using (var context = new ApplicationDbContext())
            {
                // najpierw pobieramy studenta, którego chcemy aktualizować 
                var employeeToUpdate = context.Employees.Find(employee.Id);

                
                employeeToUpdate.FirstName = employee.FirstName;
                employeeToUpdate.LastName = employee.LastName;
                employeeToUpdate.Number = employee.Number;
                employeeToUpdate.HireDate = employee.HireDate;
                employeeToUpdate.Salary = employee.Salary;
                employeeToUpdate.Released = employee.Released;
                employeeToUpdate.ReleaseDate = employee.ReleaseDate;
                employeeToUpdate.DepartmentId = employee.DepartmentId;

                try
                {
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    MessageBox.Show("Błąd zapisu do bazy");
                    return;
                }
            }
        }

        /// <summary>
        /// Usuwanie Pracownika
        /// </summary>
        /// <param name="id"></param>
        internal void DeleteEmployee(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                // var employeeToDelete = context.Employees.Where(x => x.Id == id).ToList();
                var employeeToDelete = context.Employees.Find(id);
                context.Employees.Remove(employeeToDelete);
                context.SaveChanges();
            }
        }
        //



    }


}
