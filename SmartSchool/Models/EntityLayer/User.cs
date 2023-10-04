using SmartSchool.Enums;

namespace SmartSchool.Models.EntityLayer
{
    public class User : BasePropertyChanged
    {
        private int userId;

        public int UserId
        {
            get
            {
                return userId;
            }
            set
            {
                userId = value;
                NotifyPropertyChanged(nameof(UserId));
            }
        }

        private string username;

        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                NotifyPropertyChanged(nameof(Username));
            }
        }

        private string password;

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                NotifyPropertyChanged(nameof(Password));
            }
        }


        private string firstName;

        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
                NotifyPropertyChanged(nameof(FirstName));
            }
        }

        private string lastName;

        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
                NotifyPropertyChanged(nameof(LastName));
            }
        }

        private Role role;

        public Role Role
        {
            get
            {
                return role;
            }
            set
            {
                role = value;
                NotifyPropertyChanged("Role");
            }
        }

        public string FullName => FirstName + " " + LastName;
    }
}