﻿using HumanResources.Models.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace HumanResources.Models.Wrappers
{
    public class EmployeeWrapper : IDataErrorInfo
    {

        public EmployeeWrapper()
        {
            Department = new DepartmentWrapper();
        }

        private bool _isFirstNameValid;
        private bool _isLastNameValid;

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Number { get; set; }
        public DateTime HireDate { get; set; }
        public bool Released { get; set; }
        public DateTime? ReleaseDate { get; set; }  // używamy typu nulowalnego aby móc zapisać null gdy pusta data aaa
        public decimal Salary { get; set; }
        public string PathToPhoto { get; set; }
        public DepartmentWrapper Department { get; set; }


        // nameof(FirstName) jest równoznaczne z "FirstName", ale jest sprawdzane podczas kompilacji
        // czy zmienna istnieje - zabezpieczenie na wypadek literówki, lub późniejszej zmiany nazwy zmiennej

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(FirstName):
                        {
                            if (string.IsNullOrWhiteSpace(FirstName))
                            {
                                Error = "Pole Imię jest wymagane !";
                                _isFirstNameValid = false;
                            }
                            else
                            {
                                Error = string.Empty;
                                _isFirstNameValid=true;
                            }
                        }
                        break;

                    case nameof(LastName):
                        {
                            if (string.IsNullOrWhiteSpace(LastName))
                            {
                                Error = "Pole Nazwisko jest wymagane !";
                                _isLastNameValid=false;
                            }
                            else
                            {
                                Error = string.Empty;
                                _isLastNameValid=true;
                            }
                        }
                        break;
                 
                    default:
                        break;
                }
                return Error;
            }
        }

        public string Error { get; set; }
        public bool IsValid
        {
            get
            {
                return _isFirstNameValid && _isLastNameValid && Department.IsValid;
            }
        }
    }
}
