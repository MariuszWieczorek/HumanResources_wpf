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
    }


}
