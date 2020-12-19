using HumanResources.Commands;
using HumanResources.Models;
using HumanResources.Models.Wrappers;
using HumanResources.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HumanResources.ViewModels
{
    class LoginWindowViewModel : ViewModelBase
    {
        public ICommand CloseCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }

        private Repository _repository = new Repository();

        private UserWrapper _user;
        public UserWrapper User
        {
            get { return _user; }
            set { _user = value; }
        }
        

        public LoginWindowViewModel()
        {
            CloseCommand = new RelayCommand(Close);
            ConfirmCommand = new RelayCommand(Confirm);
            User = new UserWrapper();
        }

        private void Close(object obj)
        {
            //CloseWindow(obj as Window);
            Application.Current.Shutdown();
        }

        // Czyli rzutujemy obiekt, który został przekazany na klasę LoginParams i następnie pobieramy hasło.
        // Na tą chwilę operujemy na normalny haśle, ale w przyszłości zawsze działamy na hasłach szyfrowanych
        // i tak samo w bazie powinno byc zaszyfrowane hasło.

        private void Confirm(object obj)
        {
            var loginParams = obj as LoginParams;
            User.Password = loginParams.PasswordBox.Password;

            if (Login())
            {
                CloseWindow(loginParams.Window);
            }

        }

        private void CloseWindow(Window window)
        {
            window.Close();
        }

        private bool Login()
        {
            var databaseUsers = _repository.GetUsers();
            var loginUser = databaseUsers.Where(x => x.UserName == User.UserName && x.Password == User.Password);
            if(loginUser.Count() != 0)
                return true;

            MessageBox.Show($"Brak użytkownika {User.UserName} lub nieprawidłowe hasło !");
            return false;
        }
    }
}
