using HumanResources.Commands;
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
            CloseWindow(obj as Window);
        }

        private void Confirm(object obj)
        {
            if (Login())
            {
                var x = new MainWindow();
                x.Show();
                CloseWindow(obj as Window);
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
