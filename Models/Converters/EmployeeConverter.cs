using HumanResources.Models.Domains;
using HumanResources.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HumanResources.Models.Converters
{

    // do konwersji będziemy uzywać metody rozszerzające
    // metoda rozszerzająca musi być statyczna, musi być zawarta w statycznej klasie 



    /// <summary>
    /// Metoda rozszerzająca klasę string 
    /// przykładowe wywołanie
    /// var napis = "test".ToUpper().Replace("T", "123").Trim().PadRight(20, '0').Add3x();
    /// </summary>
    public static class StringExtensions
    {
        public static string Add3x(this string model )
        {
            return model+"_3x";
        }
    }



    public static class EmployeeConverter
    {

        /// <summary>
        /// Konwersja obiektu Student na StudentWrapper
        /// Metoda rozszerzająca klasę Student ToWrapper()
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static EmployeeWrapper ToWrapper(this Employee model)
        {
            return new EmployeeWrapper
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Number = model.Number,
                Released = model.Released,
                HireDate = model.HireDate,
                ReleaseDate = model.ReleaseDate,
                Salary = model.Salary,
                PathToPhoto = model.PathToPhoto,
                Department = new DepartmentWrapper
                { 
                    Id = model.Department.Id,
                    Name = model.Department.Name
                },
                
             
            };
        }



        /// <summary>
        /// Konwersja obiektu StudentWrapper na Student
        /// Metoda rozszerzająca klasę StudentWrapper ToDao()  Data Access Object
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static Employee toDao(this EmployeeWrapper model )
        {
            return new Employee
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                HireDate = model.HireDate,
                Number = model.Number,
                Salary = model.Salary,
                Released = model.Released,
                ReleaseDate = model.ReleaseDate,
                DepartmentId = model.Department.Id,
            };
        }

    }
}
