using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Models.Domains
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Number { get; set; }
        public DateTime? HireDate { get; set; }
        public bool Released { get; set; }
        public DateTime? ReleaseDate { get; set; }  // używamy typu nulowalnego aby móc zapisać null gdy pusta data aaa
        public decimal Salary { get; set; }
        public string PathToPhoto { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
  