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


        public List<EmployeeWrapper> GetEmployees(int departmentId = 0)
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

    }


}
