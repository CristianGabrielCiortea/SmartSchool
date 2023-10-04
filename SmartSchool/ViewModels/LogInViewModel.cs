using SmartSchool.Models.EntityLayer;
using System.Windows.Input;
using SmartSchool.ViewModels.Commands;
using SmartSchool.Models.BusinessLogicLayer;

namespace SmartSchool.ViewModels
{
    public class LogInViewModel : BasePropertyChanged
    {
        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                NotifyPropertyChanged(nameof(Username));
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                NotifyPropertyChanged(nameof(Password));
            }
        }


        private ICommand logInCommand;
        public ICommand LogInCommand
        {
            get
            {
                if (logInCommand == null)
                {
                    logInCommand = new RelayCommand<User>(Register);
                }
                return logInCommand;
            }
        }

        private void Register(object param)
        {
            User loginUser = new User { Username = username, Password = password };
            SmartSchoolBLL.User.Login(loginUser);
        }
    }
}