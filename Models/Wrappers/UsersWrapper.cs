using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Models.Wrappers
{
    public class UserWrapper
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        private bool _isUserValid;
        private bool _isPasswordValid;

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {


                    case nameof(UserName):
                        {
                            if (string.IsNullOrWhiteSpace(UserName))
                            {
                                Error = "Pole użytkownik jest wymagane !";
                                _isUserValid = false;
                            }
                            else
                            {
                                Error = string.Empty;
                                _isUserValid = true;
                            }
                        }
                        break;

                    case nameof(Password):
                        {
                            if (string.IsNullOrWhiteSpace(Password))
                            {
                                Error = "Pole hasło jest wymagane !";
                                _isPasswordValid = false;
                            }
                            else
                            {
                                Error = string.Empty;
                                _isPasswordValid = true;
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
                return  _isUserValid && _isPasswordValid;
            }
        }
    }
}
